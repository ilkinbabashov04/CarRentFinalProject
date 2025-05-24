using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ui.Helper;

namespace Ui.Areas.Admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminStatistics")]
    public class AdminStatisticsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminStatisticsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
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

            #region AuthorCount
            var responseMessage3 = await client.GetAsync("https://localhost:7140/api/Statistics/GetAuthorCount");
            if (responseMessage3.IsSuccessStatusCode)
            {
                int authorCountRandom = random.Next(0, 101);
                var jsonData3 = await responseMessage3.Content.ReadAsStringAsync();
                var apiResponse3 = JsonConvert.DeserializeObject<ApiResponse<GetAuthorCountDto>>(jsonData3);
                var values3 = apiResponse3?.Data;
                ViewBag.authorCountRandom = authorCountRandom;
                ViewBag.authorCount = values3.AuthorCount;
            }
            #endregion

            #region BlogCount
            var responseMessage4 = await client.GetAsync("https://localhost:7140/api/Statistics/GetBlogCount");
            if (responseMessage4.IsSuccessStatusCode)
            {
                int blogCountRandom = random.Next(0, 101);
                var jsonData4 = await responseMessage4.Content.ReadAsStringAsync();
                var apiResponse4 = JsonConvert.DeserializeObject<ApiResponse<GetBlogCountDto>>(jsonData4);
                var values4 = apiResponse4?.Data;
                ViewBag.blogCountRandom = blogCountRandom;
                ViewBag.blogCount = values4.BlogCount;
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

            #region GetAvrRentPriceForWeekly
            var responseMessage7 = await client.GetAsync("https://localhost:7140/api/Statistics/GetAvrRentPriceForWeekly");
            if (responseMessage7.IsSuccessStatusCode)
            {
                int avrRentPriceForWeeklyRandom = random.Next(0, 101);
                var jsonData7 = await responseMessage7.Content.ReadAsStringAsync();
                var apiResponse7 = JsonConvert.DeserializeObject<ApiResponse<GetAvrRentPriceForWeeklyDto>>(jsonData7);
                var values7 = apiResponse7?.Data;
                ViewBag.avrRentPriceForWeeklyRandom = avrRentPriceForWeeklyRandom;
                ViewBag.avrRentPriceForWeekly = values7.AverageWeeklyRentPrice.ToString("0.00");
            }
            #endregion

            #region GetAvrRentPriceForMonthly
            var responseMessage8 = await client.GetAsync("https://localhost:7140/api/Statistics/GetAvrRentPriceForMonthly");
            if (responseMessage8.IsSuccessStatusCode)
            {
                int avrRentPriceForMonthlyRandom = random.Next(0, 101);
                var jsonData8 = await responseMessage8.Content.ReadAsStringAsync();
                var apiResponse8 = JsonConvert.DeserializeObject<ApiResponse<GetAvrRentPriceForMonthlyDto>>(jsonData8);
                var values8 = apiResponse8?.Data;
                ViewBag.avrRentPriceForMonthlyRandom = avrRentPriceForMonthlyRandom;
                ViewBag.avrRentPriceForMonthly = values8.AverageMonthlyRentPrice.ToString("0.00");
            }
            #endregion

            #region GetCarCountByTransmissionIsAuto
            var responseMessage9 = await client.GetAsync("https://localhost:7140/api/Statistics/GetCarCountByTransmissionIsAuto");
            if (responseMessage8.IsSuccessStatusCode)
            {
                int carCountByTransmissionIsAutoRandom = random.Next(0, 101);
                var jsonData9 = await responseMessage9.Content.ReadAsStringAsync();
                var apiResponse9 = JsonConvert.DeserializeObject<ApiResponse<GetCarCountByTransmissionIsAutoDto>>(jsonData9);
                var values9 = apiResponse9?.Data;
                ViewBag.carCountByTransmissionIsAutoRandom = carCountByTransmissionIsAutoRandom;
                ViewBag.carCountByTransmissionIsAuto = values9.AutomaticCarCount;
            }
            #endregion

            #region GetCarCountByKmLessThan1000
            var responseMessage10 = await client.GetAsync("https://localhost:7140/api/Statistics/GetCarCountByKmLessThan1000");
            if (responseMessage10.IsSuccessStatusCode)
            {
                int carCountByKmLessThan1000Random = random.Next(0, 101);
                var jsonData10 = await responseMessage10.Content.ReadAsStringAsync();
                var apiResponse10 = JsonConvert.DeserializeObject<ApiResponse<GetCarCountByKmLessThan1000Dto>>(jsonData10);
                var values10 = apiResponse10?.Data;
                ViewBag.carCountByKmLessThan1000Random = carCountByKmLessThan1000Random;
                ViewBag.carCountByKmLessThan1000 = values10.CarCount;
            }
            #endregion

            #region GetCarCountByFuelGasolineOrDiesel
            var responseMessage11 = await client.GetAsync("https://localhost:7140/api/Statistics/GetCarCountByFuelGasolineOrDiesel");
            if (responseMessage11.IsSuccessStatusCode)
            {
                int carCountByFuelGasolineOrDieselRandom = random.Next(0, 101);
                var jsonData11 = await responseMessage11.Content.ReadAsStringAsync();
                var apiResponse11 = JsonConvert.DeserializeObject<ApiResponse<GetCarCountByFuelGasolineOrDieselDto>>(jsonData11);
                var values11 = apiResponse11?.Data;
                ViewBag.carCountByFuelGasolineOrDieselRandom = carCountByFuelGasolineOrDieselRandom;
                ViewBag.carCountByFuelGasolineOrDiesel = values11.CarCountByFuelGasolineOrDiesel;
            }
            #endregion

            #region GetCarCountByFuelElectric
            var responseMessage12 = await client.GetAsync("https://localhost:7140/api/Statistics/GetCarCountByFuelElectric");
            if (responseMessage12.IsSuccessStatusCode)
            {
                int carCountByFuelElectricRandom = random.Next(0, 101);
                var jsonData12 = await responseMessage12.Content.ReadAsStringAsync();
                var apiResponse12 = JsonConvert.DeserializeObject<ApiResponse<GetCarCountByFuelElectricDto>>(jsonData12);
                var values12 = apiResponse12?.Data;
                ViewBag.carCountByFuelElectricRandom = carCountByFuelElectricRandom;
                ViewBag.carCountByFuelElectric = values12.CarCountByFuelElectric;
            }
            #endregion

            #region GetCarBrandAndModelByDailyRentPriceIsMax
            var responseMessage13 = await client.GetAsync("https://localhost:7140/api/Statistics/GetCarBrandAndModelByDailyRentPriceIsMax");
            if (responseMessage13.IsSuccessStatusCode)
            {
                var jsonData13 = await responseMessage13.Content.ReadAsStringAsync();
                var apiResponse13 = JsonConvert.DeserializeObject<ApiResponse<GetCarBrandAndModelByDailyRentPriceIsMaxDto>>(jsonData13);
                var values13 = apiResponse13?.Data;
                ViewBag.carAmountByDailyRentPriceIsMax = values13.Amount;
                ViewBag.carBrandByDailyRentPriceIsMax = values13.CarModel;
            }
            #endregion

            #region GetCarBrandAndModelByDailyRentPriceIsMin
            var responseMessage14 = await client.GetAsync("https://localhost:7140/api/Statistics/GetCarBrandAndModelByDailyRentPriceIsMin");
            if (responseMessage14.IsSuccessStatusCode)
            {
                var jsonData14 = await responseMessage14.Content.ReadAsStringAsync();
                var apiResponse14 = JsonConvert.DeserializeObject<ApiResponse<GetCarBrandAndModelByDailyRentPriceIsMinDto>>(jsonData14);
                var values14 = apiResponse14?.Data;
                ViewBag.carAmountByDailyRentPriceIsMin = values14.Amount;
                ViewBag.carBrandByDailyRentPriceIsMin = values14.CarModel;
            }
            #endregion

            #region GetBrandNameByMaxCar
            var responseMessage15 = await client.GetAsync("https://localhost:7140/api/Statistics/GetBrandNameByMaxCar");
            if (responseMessage15.IsSuccessStatusCode)
            {
                var jsonData15 = await responseMessage15.Content.ReadAsStringAsync();
                var apiResponse15 = JsonConvert.DeserializeObject<ApiResponse<GetBrandNameByMaxCarDto>>(jsonData15);
                var values15 = apiResponse15?.Data;
                ViewBag.BrandNameByMaxCar = values15.BrandName;
            }
            #endregion

            #region BlogTitleByMaxBlogComment
            var responseMessage16 = await client.GetAsync("https://localhost:7140/api/Statistics/BlogTitleByMaxBlogComment");
            if (responseMessage16.IsSuccessStatusCode)
            {
                var jsonData16 = await responseMessage16.Content.ReadAsStringAsync();
                var apiResponse16 = JsonConvert.DeserializeObject<ApiResponse<BlogTitleByMaxBlogCommentDto>>(jsonData16);
                var values16 = apiResponse16?.Data;
                ViewBag.BlogTitleByMaxBlogComment = values16.BlogTitle;
            }
            #endregion

            return View();
            
        }
    }
}
