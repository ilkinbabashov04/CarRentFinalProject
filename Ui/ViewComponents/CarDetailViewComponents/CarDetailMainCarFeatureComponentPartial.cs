using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using Ui.Helper;

namespace Ui.ViewComponents.CarDetailViewComponents
{
	public class CarDetailMainCarFeatureComponentPartial : ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public CarDetailMainCarFeatureComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync(int id)
		{
			ViewBag.carId = id;
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"https://localhost:7140/api/Car/GetCarById {id}");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				Console.WriteLine($"API Response: {jsonData}");
				var apiResponse = JsonConvert.DeserializeObject<ApiResponse<CarDto>>(jsonData);
				var values = apiResponse?.Data;
				return View(values);
			}
			return View();
		}
	}
}
