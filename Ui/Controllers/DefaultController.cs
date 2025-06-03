﻿using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Ui.Helper;

namespace Ui.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7140/api/Location/GetAll");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<LocationDto>>>(jsonData);
            var values = apiResponse?.Data;
            List<SelectListItem> values2 = (from x in values
                                            select new SelectListItem
                                            {
                                                Text = x.Name,
                                                Value = x.Id.ToString(),
                                            }).ToList();
            ViewBag.v = values2;
            ViewBag.LocationData = values;
            return View();
        }
        [HttpPost]
        public IActionResult Index(string pickup_date, string dropoff_date, string time_pick, string time_off, string Id)
        {
            TempData["bookpickdate"] = pickup_date;
            TempData["bookoffdate"] = dropoff_date;
            TempData["timepick"] = time_pick;
            TempData["timeoff"] = time_off;
            TempData["Id"] = Id;
            return RedirectToAction("Index", "RentACarList");
        }
    }
}
