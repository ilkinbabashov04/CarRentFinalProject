using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Ui.Helper;

namespace Ui.Areas.Admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminSocialMedia")]
    public class AdminSocialMediaController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminSocialMediaController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7140/api/SocialMedia/GetAll");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<SocialMediaDto>>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }

        [HttpGet]
        [Route("CreateSocialMedia")]
        public IActionResult CreateSocialMedia()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateSocialMedia")]
        public async Task<IActionResult> CreateSocialMedia(CreateSocialMediaDto createSocialMediaDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createSocialMediaDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.PostAsync("https://localhost:7140/api/SocialMedia/AddSocialMedia", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminSocialMedia", new { area = "Admin" });
                }
            }
            return View();
        }

        [Route("DeleteSocialMedia/{id}")]
        public async Task<IActionResult> DeleteSocialMedia(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.DeleteAsync($"https://localhost:7140/api/SocialMedia/DeleteSocialMedia?id={id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminSocialMedia", new { area = "Admin" });
                }
            }
            return RedirectToAction("Index", "AdminSocialMedia", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateSocialMedia/{id}")]
        public async Task<IActionResult> UpdateSocialMedia(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync($"https://localhost:7140/api/SocialMedia/GetSocialMediaById?id={id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {jsonData}");
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<SocialMediaDto>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }


        [HttpPost]
        [Route("UpdateSocialMedia/{id}")]
        public async Task<IActionResult> UpdateSocialMedia(SocialMediaDto SocialMediaDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(SocialMediaDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.PostAsync("https://localhost:7140/api/SocialMedia/UpdateSocialMedia", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminSocialMedia", new { area = "Admin" });
                }
            }
            return View();
        }
    }
}
