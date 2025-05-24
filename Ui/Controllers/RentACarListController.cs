using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Ui.Helper;

namespace Ui.Controllers
{
    public class RentACarListController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RentACarListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var Id = TempData["Id"];
            var bookPickDate = TempData["bookpickdate"];
            var bookOffDate = TempData["bookoffdate"];
            var timePick = TempData["timepick"];
            var timeOff = TempData["timeoff"];

            ViewBag.Id = Id;
            ViewBag.bookPickDate = bookPickDate;
            ViewBag.bookOffDate = bookOffDate;
            ViewBag.timePick = timePick;
            ViewBag.timeOff = timeOff;

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7140/api/Car/GetAvailableCars" +
                $"?locationId={Id}" +
                $"&pickupDateTime={bookPickDate:yyyy-MM-dd}" +
                $"&dropoffDateTime={bookOffDate:yyyy-MM-dd}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<CarGetByFilterDto>>>(jsonData);
                var values = apiResponse?.Data;
                return View(values);
            }
            return View();
        }
    }
}
