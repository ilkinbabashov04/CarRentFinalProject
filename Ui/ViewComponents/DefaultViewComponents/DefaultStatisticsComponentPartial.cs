using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using Ui.Helper;

namespace Ui.ViewComponents.DefaultViewComponents
{
    public class DefaultStatisticsComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultStatisticsComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            #region CarCount
            var responseMessage = await client.GetAsync("https://localhost:7140/api/Statistics/GetCarCount");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<GetCarCountDto>>(jsonData);
                var values = apiResponse?.Data;
                ViewBag.carCount = values.CarCount;
            }
            #endregion

            #region LocationCount
            var responseMessage2 = await client.GetAsync("https://localhost:7140/api/Statistics/GetLocationCount");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
                var apiResponse2 = JsonConvert.DeserializeObject<ApiResponse<GetLocationCountDto>>(jsonData2);
                var values2 = apiResponse2?.Data;
                ViewBag.locationCount = values2.LocationCount;
            }
            #endregion

            #region BrandCount
            var responseMessage3 = await client.GetAsync("https://localhost:7140/api/Statistics/GetBrandCount");
            if (responseMessage3.IsSuccessStatusCode)
            {
                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
                var apiResponse3 = JsonConvert.DeserializeObject<ApiResponse<GetBrandCountDto>>(jsonData3);
                var values3 = apiResponse3?.Data;
                ViewBag.brandCount = values3.BrandCount;
            }
            #endregion

            #region GetCarCountByFuelElectric
            var responseMessage4 = await client.GetAsync("https://localhost:7140/api/Statistics/GetCarCountByFuelElectric");
            if (responseMessage4.IsSuccessStatusCode)
            {
                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
                var apiResponse4 = JsonConvert.DeserializeObject<ApiResponse<GetCarCountByFuelElectricDto>>(jsonData4);
                var values4 = apiResponse4?.Data;
                ViewBag.carCountByFuelElectric = values4.CarCountByFuelElectric;
            }
            #endregion
            return View();
        }
    }
}