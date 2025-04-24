using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Ui.Helper;

namespace Ui.Areas.Admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Area("Admin")]
    [Route("Admin/AdminCarFeatureDetail")]
    public class AdminCarFeatureDetailController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminCarFeatureDetailController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("Index/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7140/api/CarFeature/GetCarFeatureByCarId?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<CarFeatureDto>>>(jsonData);
                var values = apiResponse?.Data;
                return View(values);
            }
            return View();
        }
        [HttpPost]
        [Route("Index/{id}")]
        public async Task<IActionResult> Index(List<CarFeatureDto> carFeatureDto)
        {
            foreach (var item in carFeatureDto)
            {
                if (item.Available)
                {
                    var client = _httpClientFactory.CreateClient();
                    var responseMessage = await client.GetAsync("https://localhost:7140/api/CarFeature/CarFeatureChangeAvailableToTrue?id=" + item.Id);
                }
                else
                {
                    var client = _httpClientFactory.CreateClient();
                    await client.GetAsync("https://localhost:7140/api/CarFeature/CarFeatureChangeAvailableToFalse?id=" + item.Id);
                }
            }
            return RedirectToAction("Index", "AdminCar");
        }
        [Route("CreateFeatureByCarId")]
        [HttpGet]
        public async Task<IActionResult> CreateFeatureByCarId()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7140/api/Feature/GetAll");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<FeatureDto>>>(jsonData);
                var values = apiResponse?.Data;
                return View(values);
            }
            return View();
        }
    }
}
