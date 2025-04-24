﻿using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ui.Helper;

namespace Ui.Controllers
{
    public class CarPricingController : Controller
    {
		private readonly IHttpClientFactory _httpClientFactory;

		public CarPricingController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Pricing Packets";
            ViewBag.v1 = "Car Pricing Packets";

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7140/api/CarPricing/GetCarPricingWithTimePeriods");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<GetCarPricingWithTimePeriodDto>>>(jsonData);
				var values = apiResponse?.Data;
				return View(values);
			}

			return View();
		}
    }
}
