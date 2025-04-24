using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ui.Helper;

namespace Ui.ViewComponents.DashboardComponents
{
    public class AdminDashboardStatisticsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminDashboardStatisticsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Random random = new Random();
            var client = _httpClientFactory.CreateClient();

            #region CarCount
            var responseMessage = await client.GetAsync("https://localhost:7140/api/Statistics/GetCarCount");
            if (responseMessage.IsSuccessStatusCode)
            {
                int v1 = random.Next(0, 101);
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<GetCarCountDto>>(jsonData);
                var values = apiResponse?.Data;
                ViewBag.v1 = v1;
                ViewBag.v = values.CarCount;
            }
            #endregion

            #region LocationCount
            var responseMessage2 = await client.GetAsync("https://localhost:7140/api/Statistics/GetLocationCount");
            if (responseMessage.IsSuccessStatusCode)
            {
                int locationCountRandom = random.Next(0, 101);
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                var apiResponse2 = JsonConvert.DeserializeObject<ApiResponse<GetLocationCountDto>>(jsonData2);
                var values2 = apiResponse2?.Data;
                ViewBag.locationCountRandom = locationCountRandom;
                ViewBag.locationCount = values2.LocationCount;
            }

            #endregion

            #region BrandCount
            var responseMessage5 = await client.GetAsync("https://localhost:7140/api/Statistics/GetBrandCount");
            if (responseMessage5.IsSuccessStatusCode)
            {
                int brandCountRandom = random.Next(0, 101);
                var jsonData5 = await responseMessage5.Content.ReadAsStringAsync();
                var apiResponse5 = JsonConvert.DeserializeObject<ApiResponse<GetBrandCountDto>>(jsonData5);
                var values5 = apiResponse5?.Data;
                ViewBag.brandCountRandom = brandCountRandom;
                ViewBag.brandCount = values5.BrandCount;
            }

            #endregion

            #region GetAvrRentPriceForDaily
            var responseMessage6 = await client.GetAsync("https://localhost:7140/api/Statistics/GetAvrRentPriceForDaily");
            if (responseMessage6.IsSuccessStatusCode)
            {
                int avrRentPriceForDailyRandom = random.Next(0, 101);
                var jsonData6 = await responseMessage6.Content.ReadAsStringAsync();
                var apiResponse6 = JsonConvert.DeserializeObject<ApiResponse<GetAvrRentPriceForDailyDto>>(jsonData6);
                var values6 = apiResponse6?.Data;
                ViewBag.avrRentPriceForDailyRandom = avrRentPriceForDailyRandom;
                ViewBag.avrRentPriceForDaily = values6.AverageDailyRentPrice.ToString("0.00");
            }
            #endregion

            return View();

        }
    }
}
