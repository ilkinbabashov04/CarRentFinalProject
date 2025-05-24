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
    [Route("Admin/AdminAuthor")]
    public class AdminAuthorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminAuthorController(IHttpClientFactory httpClientFactory)
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
                var responseMessage = await client.GetAsync("https://localhost:7140/api/Author/GetAll");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<AuthorDto>>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }

        [HttpGet]
        [Route("CreateAuthor")]
        public IActionResult CreateAuthor()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateAuthor")]
        public async Task<IActionResult> CreateAuthor(CreateAuthorDto createAuthorDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createAuthorDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.PostAsync("https://localhost:7140/api/Author/AddAuthor", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminAuthor", new { area = "Admin" });
                }
            }
            return View();
        }

        [Route("DeleteAuthor/{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.DeleteAsync($"https://localhost:7140/api/Author/DeleteAuthor?id={id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminAuthor", new { area = "Admin" });
                }
            }
            return RedirectToAction("Index", "AdminAuthor", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"https://localhost:7140/api/Author/GetAuthorById?id={id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {jsonData}");
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<AuthorDto>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }


        [HttpPost]
        [Route("UpdateAuthor/{id}")]
        public async Task<IActionResult> UpdateAuthor(AuthorDto AuthorDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(AuthorDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.PostAsync("https://localhost:7140/api/Author/UpdateAuthor", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminAuthor", new { area = "Admin" });
                }
            }
            return View();
        }
    }
}
