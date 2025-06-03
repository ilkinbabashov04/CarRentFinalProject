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
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync("https://localhost:7140/api/CarFeature/GetCarFeatureByCarId?id=" + id);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<CarFeatureDto>>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }
        [HttpPost]
        [Route("Index/{id}")]
        public async Task<IActionResult> Index(List<CarFeatureDto> carFeatureDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                foreach (var item in carFeatureDto)
                {
                    if (item.Available)
                    {
                        var client = _httpClientFactory.CreateClient();
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        var responseMessage = await client.GetAsync("https://localhost:7140/api/CarFeature/CarFeatureChangeAvailableToTrue?id=" + item.Id);
                    }
                    else
                    {
                        var client = _httpClientFactory.CreateClient();
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        await client.GetAsync("https://localhost:7140/api/CarFeature/CarFeatureChangeAvailableToFalse?id=" + item.Id);
                    }
                }
            }
            return RedirectToAction("Index", "AdminCar");
        }
        [Route("CreateFeatureByCarId/{id}")]
        [HttpGet]
        public async Task<IActionResult> CreateFeatureByCarId(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7140/api/Feature/GetAll");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<FeatureDto>>>(jsonData);
                    var values = apiResponse?.Data;
                    ViewBag.id = id;
                    return View(values);
                }
            }
            return View();
        }


        [HttpPost]
        [Route("CreateFeatureByCarId/{id}")]
        public async Task<IActionResult> CreateFeatureByCarId(int id, List<FeatureDto> features)
        {
            var selectedFeatures = features.Where(f => f.IsSelected).ToList();

            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null && selectedFeatures.Any())
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                foreach (var feature in selectedFeatures)
                {
                    var dto = new CreateCarFeatureDto
                    {
                        CarId = id,
                        FeatureId = feature.Id,
                        FeatureName = feature.Name
                    };

                    var jsonData = JsonConvert.SerializeObject(dto);
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"https://localhost:7140/api/CarFeature/AddCarFeature", content);
                }

                return RedirectToAction("Index", "AdminCar");
            }

            return View();
        }

    }
}
