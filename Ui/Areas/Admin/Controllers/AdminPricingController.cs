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
    [Route("Admin/AdminPricing")]
    public class AdminPricingController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminPricingController(IHttpClientFactory httpClientFactory)
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
                var responseMessage = await client.GetAsync("https://localhost:7140/api/Pricing/GetAll");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<PricingDto>>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }

        [HttpGet]
        [Route("CreatePricing")]
        public IActionResult CreatePricing()
        {
            return View();
        }

        [HttpPost]
        [Route("CreatePricing")]
        public async Task<IActionResult> CreatePricing(CreatePricingDto createPricingDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createPricingDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.PostAsync("https://localhost:7140/api/Pricing/AddPricing", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminPricing", new { area = "Admin" });
                }
            }
            return View();
        }

        [Route("DeletePricing/{id}")]
        public async Task<IActionResult> DeletePricing(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.DeleteAsync($"https://localhost:7140/api/Pricing/DeletePricing?id={id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminPricing", new { area = "Admin" });
                }
            }
            return RedirectToAction("Index", "AdminPricing", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdatePricing/{id}")]
        public async Task<IActionResult> UpdatePricing(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync($"https://localhost:7140/api/Pricing/GetPricingById?id={id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {jsonData}");
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<PricingDto>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }


        [HttpPost]
        [Route("UpdatePricing/{id}")]
        public async Task<IActionResult> UpdatePricing(PricingDto PricingDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(PricingDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.PostAsync("https://localhost:7140/api/Pricing/UpdatePricing", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminPricing", new { area = "Admin" });
                }
            }
            return View();
        }
    }
}
