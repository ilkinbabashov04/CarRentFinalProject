using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Ui.Helper;

namespace Ui.Areas.Admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Area("Admin")]
    [Route("Admin/AdminHome")]
    public class AdminHomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminHomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7140/api/Home/GetAll");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<HomeDto>>>(jsonData);
                var values = apiResponse?.Data;
                return View(values);
            }
            return View();
        }

        [HttpGet]
        [Route("CreateHome")]
        public IActionResult CreateHome()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateHome")]
        public async Task<IActionResult> CreateHome(CreateHomeDto createHomeDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createHomeDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7140/api/Home/AddHome", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
            }
            return View();
        }

        [Route("DeleteHome/{id}")]
        public async Task<IActionResult> DeleteHome(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7140/api/Home/DeleteHome/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
            }
            return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateHome/{id}")]
        public async Task<IActionResult> UpdateHome(int id)
        {

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7140/api/Home/GetHomeById?id={id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response: {jsonData}");
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<HomeDto>>(jsonData);
                var values = apiResponse?.Data;
                return View(values);
            }
            return View();
        }


        [HttpPost]
        [Route("UpdateHome/{id}")]
        public async Task<IActionResult> UpdateHome(HomeDto homeDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(homeDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7140/api/Home/UpdateHome", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "AdminHome", new { area = "Admin" });
            }
            return View();
        }
    }
}
