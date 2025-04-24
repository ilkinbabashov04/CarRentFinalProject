using Entities.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Ui.Models;

namespace Ui.Controllers
{
	public class LoginController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;
		public LoginController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(LoginDTO loginDto)
		{
			if (!ModelState.IsValid)
			{
				return View(loginDto); // Return the view with validation errors
			}

			var client = _httpClientFactory.CreateClient();
			var content = new StringContent(JsonSerializer.Serialize(loginDto), Encoding.UTF8, "application/json");
			var response = await client.PostAsync("https://localhost:7140/api/Login", content);

			if (response.IsSuccessStatusCode)
			{
				var jsonData = await response.Content.ReadAsStringAsync();
				var tokenModel = JsonSerializer.Deserialize<JwtResponseModel>(jsonData, new JsonSerializerOptions
				{
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				});

				if (tokenModel != null)
				{
					if (tokenModel.ExpireDate <= DateTime.UtcNow)
					{
						ModelState.AddModelError("", "Session has expired. Please log in again.");
						return View(loginDto);
					}

					JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
					var token = handler.ReadJwtToken(tokenModel.Token);
					var claims = token.Claims.ToList();

					if (tokenModel.Token != null)
					{
						claims.Add(new Claim("accessToken", tokenModel.Token));
						var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
						var authProps = new AuthenticationProperties
						{
							ExpiresUtc = tokenModel.ExpireDate,
							IsPersistent = true
						};

						await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProps);
						return RedirectToAction("Index", "Default");
					}
				}
			}
			else
			{
				ModelState.AddModelError("", "Invalid login attempt. Please check your email and password.");
				return View(loginDto);
			}

			return View();
		}

	}
}
