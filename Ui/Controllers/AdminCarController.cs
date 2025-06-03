using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using Ui.Helper;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Ui.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=babashovilkin;AccountKey=aNQF8IbAitLm9STdiV7A0py55M9c70bW9xYmO4dBpQnlFJe7SpkC1OcUi+46jZFtzoGMariWaJMH+AStM4+8hw==;EndpointSuffix=core.windows.net";
        private readonly string shareName = "cars";
        public AdminCarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7140/api/Car/GetAll");
                
                
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<CarDto>>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateCar()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7140/api/Brand/GetAll");
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response: {jsonData}");
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<BrandDto>>>(jsonData);
                var values = apiResponse?.Data;
                List<SelectListItem> brandValues = (from x in values
                                                    select new SelectListItem
                                                    {
                                                        Text = x.Name,
                                                        Value = x.BrandId.ToString()
                                                    }).ToList();

                ViewBag.BrandValues = brandValues;
                ViewBag.TransmissionOptions = new List<SelectListItem>
        {
            new SelectListItem { Text = "Automatic", Value = "Automatic" },
            new SelectListItem { Text = "Manual", Value = "Manual" }
        };

                ViewBag.FuelOptions = new List<SelectListItem>
        {
            new SelectListItem { Text = "Diesel", Value = "Diesel" },
            new SelectListItem { Text = "Gasoline", Value = "Gasoline" },
            new SelectListItem { Text = "Hybrid", Value = "Hybrid" },
            new SelectListItem { Text = "Electric", Value = "Electric" }
        };
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CarFileDto createCarDto, IFormFile BigImage, IFormFile CoverImage)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                using (var formData = new MultipartFormDataContent())
                {
                    formData.Add(new StringContent(createCarDto.BrandId.ToString()), "BrandId");
                    formData.Add(new StringContent(createCarDto.Model), "Model");
                    formData.Add(new StringContent(createCarDto.Km.ToString()), "Km");
                    formData.Add(new StringContent(createCarDto.Transmission), "Transmission");
                    formData.Add(new StringContent(createCarDto.Seat.ToString()), "Seat");
                    formData.Add(new StringContent(createCarDto.Luggage.ToString()), "Luggage");
                    formData.Add(new StringContent(createCarDto.Fuel), "Fuel");
                    formData.Add(new StringContent(createCarDto.Description), "Description");
                    formData.Add(new StringContent(createCarDto.PerDayPrice.ToString()), "PerDayPrice");
                    formData.Add(new StringContent(createCarDto.PerWeekPrice.ToString()), "PerWeekPrice");
                    formData.Add(new StringContent(createCarDto.PerMonthPrice.ToString()), "PerMonthPrice");

                    if (BigImage != null)
                    {
                        var bigImageContent = new StreamContent(BigImage.OpenReadStream());
                        bigImageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(BigImage.ContentType);
                        formData.Add(bigImageContent, "bigImage", BigImage.FileName);
                    }

                    if (CoverImage != null)
                    {
                        var coverImageContent = new StreamContent(CoverImage.OpenReadStream());
                        coverImageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(CoverImage.ContentType);
                        formData.Add(coverImageContent, "coverImage", CoverImage.FileName);
                    }
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                    var responseMessage = await client.PostAsync("https://localhost:7140/api/Car/AddCar", formData);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var responseContent = await responseMessage.Content.ReadAsStringAsync();
                        var json = JObject.Parse(responseContent);

                        int carId = (int)json["id"];

                        var descriptionDto = new
                        {
                            CarId = carId,
                            Detail = createCarDto.Description
                        };

                        var descriptionContent = new StringContent(
                            JsonConvert.SerializeObject(descriptionDto),
                            Encoding.UTF8,
                            "application/json"
                        );

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        var descResponse = await client.PostAsync("https://localhost:7140/api/CarDescription/AddCarDescription", descriptionContent);

                        var pricingDto = new
                        {
                            CarId = carId,
                            PricingId = 1, // Per Day
                            Amount = createCarDto.PerDayPrice
                        };

                        var pricingContent = new StringContent(
                            JsonConvert.SerializeObject(pricingDto),
                            Encoding.UTF8,
                            "application/json"
                        );
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        await client.PostAsync("https://localhost:7140/api/CarPricing/AddCarPricing", pricingContent);

                        var pricingDto2 = new
                        {
                            CarId = carId,
                            PricingId = 2, // Per week
                            Amount = createCarDto.PerWeekPrice
                        };

                        var pricingContent2 = new StringContent(
                            JsonConvert.SerializeObject(pricingDto2),
                            Encoding.UTF8,
                            "application/json"
                        );
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        await client.PostAsync("https://localhost:7140/api/CarPricing/AddCarPricing", pricingContent2);

                        var pricingDto3 = new
                        {
                            CarId = carId,
                            PricingId = 4, // Per Month
                            Amount = createCarDto.PerMonthPrice
                        };

                        var pricingContent3 = new StringContent(
                            JsonConvert.SerializeObject(pricingDto3),
                            Encoding.UTF8,
                            "application/json"
                        );
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        await client.PostAsync("https://localhost:7140/api/CarPricing/AddCarPricing", pricingContent3);
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }


        public async Task<IActionResult> DeleteCar(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var responseMessage = await client.DeleteAsync($"https://localhost:7140/api/Car/DeleteCar/{id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage1 = await client.GetAsync("https://localhost:7140/api/Brand/GetAll");
                var jsonData1 = await responseMessage1.Content.ReadAsStringAsync();
                Console.WriteLine($"API Response: {jsonData1}");
                var apiResponse1 = JsonConvert.DeserializeObject<ApiResponse<List<BrandDto>>>(jsonData1);
                var values1 = apiResponse1?.Data;
                List<SelectListItem> brandValues = (from x in values1
                                                    select new SelectListItem
                                                    {
                                                        Text = x.Name,
                                                        Value = x.BrandId.ToString()
                                                    }).ToList();

                ViewBag.BrandValues = brandValues;
                ViewBag.TransmissionOptions = new List<SelectListItem>
        {
            new SelectListItem { Text = "Automatic", Value = "Automatic" },
            new SelectListItem { Text = "Manual", Value = "Manual" }
        };

                ViewBag.FuelOptions = new List<SelectListItem>
        {
            new SelectListItem { Text = "Diesel", Value = "Diesel" },
            new SelectListItem { Text = "Gasoline", Value = "Gasoline" },
            new SelectListItem { Text = "Hybrid", Value = "Hybrid" },
            new SelectListItem { Text = "Electric", Value = "Electric" }
        };

                var responseMessage = await client.GetAsync($"https://localhost:7140/api/Car/GetCarById {id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {jsonData}");
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<CarDto>>(jsonData);
                    var values = apiResponse?.Data;

                    var descriptionResponse = await client.GetAsync($"https://localhost:7140/api/CarDescription/GetCarDescriptionByCarId?id={id}");
                    if (descriptionResponse.IsSuccessStatusCode)
                    {
                        var descriptionJson = await descriptionResponse.Content.ReadAsStringAsync();
                        var descriptionApiResponse = JsonConvert.DeserializeObject<ApiResponse<CarDescriptionDto>>(descriptionJson);
                        if (descriptionApiResponse?.Data != null)
                        {
                            values.Description = descriptionApiResponse.Data.Detail;
                        }
                    }
                    // 5. CarPricing getir və PerDay, PerWeek, PerMonth sahələrinə doldur
                    var pricingResponse = await client.GetAsync($"https://localhost:7140/api/CarPricing/GetCarPricingByCarId?id={id}");
                    if (pricingResponse.IsSuccessStatusCode)
                    {
                        var pricingJson = await pricingResponse.Content.ReadAsStringAsync();
                        var pricingApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<CarPricingDto>>>(pricingJson);
                        var pricingData = pricingApiResponse?.Data;

                        if (pricingData != null)
                        {
                            foreach (var pricing in pricingData)
                            {
                                switch (pricing.PricingId)
                                {
                                    case 1:
                                        values.PerDayPrice = pricing.Amount;
                                        break;
                                    case 2:
                                        values.PerWeekPrice = pricing.Amount;
                                        break;
                                    case 4:
                                        values.PerMonthPrice = pricing.Amount;
                                        break;
                                }
                            }
                        }
                    }

                    return View(values);
                }
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCar(CarDto carDto, IFormFile? CoverImage, IFormFile? BigImage)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                // Fetch Brand Name
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var brandResponseMessage = await client.GetAsync($"https://localhost:7140/api/Brand/GetBrandById?id={carDto.BrandId}");
                if (brandResponseMessage.IsSuccessStatusCode)
                {
                    var brandJsonData = await brandResponseMessage.Content.ReadAsStringAsync();
                    var brandApiResponse = JsonConvert.DeserializeObject<ApiResponse<BrandDto>>(brandJsonData);
                    if (brandApiResponse?.Data != null)
                    {
                        carDto.BrandName = brandApiResponse.Data.Name;
                    }
                }

                using var formData = new MultipartFormDataContent();

                // Add text fields
                formData.Add(new StringContent(carDto.BrandId.ToString()), "BrandId");
                formData.Add(new StringContent(carDto.Model), "Model");
                formData.Add(new StringContent(carDto.Km.ToString()), "Km");
                formData.Add(new StringContent(carDto.Transmission), "Transmission");
                formData.Add(new StringContent(carDto.Seat.ToString()), "Seat");
                formData.Add(new StringContent(carDto.Luggage.ToString()), "Luggage");
                formData.Add(new StringContent(carDto.Fuel), "Fuel");
                formData.Add(new StringContent(carDto.Description), "Description");
                //formData.Add(new StringContent(carDto.PerDayPrice.ToString()), "PerDayPrice");

                // Add image files if present
                if (CoverImage != null)
                {
                    var coverImageStream = new StreamContent(CoverImage.OpenReadStream());
                    formData.Add(coverImageStream, "coverImage", CoverImage.FileName);
                }

                if (BigImage != null)
                {
                    var bigImageStream = new StreamContent(BigImage.OpenReadStream());
                    formData.Add(bigImageStream, "bigImage", BigImage.FileName);
                }
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                await client.PostAsync($"https://localhost:7140/api/Car/UpdateCar/{carDto.id}", formData);
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var getDescriptionResponse = await client.GetAsync($"https://localhost:7140/api/CarDescription/GetCarDescriptionByCarId?id={carDto.id}");
                if (getDescriptionResponse.IsSuccessStatusCode)
                {
                    var getDescriptionJson = await getDescriptionResponse.Content.ReadAsStringAsync();
                    var getDescriptionData = JsonConvert.DeserializeObject<ApiResponse<CarDescriptionDto>>(getDescriptionJson);

                    if (getDescriptionData?.Data != null)
                    {
                        var descriptionDto = new
                        {
                            Id = getDescriptionData.Data.Id, 
                            CarId = carDto.id,
                            Detail = carDto.Description
                        };

                        var descriptionContent = new StringContent(
                            JsonConvert.SerializeObject(descriptionDto),
                            Encoding.UTF8,
                            "application/json"
                        );
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        var responseMessage2 = await client.PostAsync("https://localhost:7140/api/CarDescription/UpdateCarDescription", descriptionContent);
                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                        var pricingResponse = await client.GetAsync($"https://localhost:7140/api/CarPricing/GetCarPricingByCarId?id={carDto.id}");
                        var pricingJson = await pricingResponse.Content.ReadAsStringAsync();
                        var pricingData = JsonConvert.DeserializeObject<ApiResponse<List<CarPricingDto>>>(pricingJson);

                        if (pricingData?.Data != null)
                        {
                            var perDay = pricingData.Data.FirstOrDefault(p => p.PricingId == 1); // Günlük
                            var perWeek = pricingData.Data.FirstOrDefault(p => p.PricingId == 2); // Həftəlik
                            var perMonth = pricingData.Data.FirstOrDefault(p => p.PricingId == 4); // Aylıq

                            // Günlük qiyməti güncəllə
                            if (perDay != null)
                            {
                                var dayDto = new
                                {
                                    Id = perDay.CarPricingId,
                                    CarId = carDto.id,
                                    PricingId = 1,
                                    Amount = carDto.PerDayPrice,
                                    IsDelete = false
                                };

                                var dayContent = new StringContent(
                                    JsonConvert.SerializeObject(dayDto),
                                    Encoding.UTF8,
                                    "application/json"
                                );
                                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                                var ressss = await client.PostAsync("https://localhost:7140/api/CarPricing/UpdateCarPricing", dayContent);
                            }

                            // Həftəlik
                            if (perWeek != null)
                            {
                                var weekDto = new
                                {
                                    Id = perWeek.CarPricingId,
                                    CarId = carDto.id,
                                    PricingId = 2,
                                    Amount = carDto.PerWeekPrice,
                                    IsDelete = false
                                };

                                var weekContent = new StringContent(
                                    JsonConvert.SerializeObject(weekDto),
                                    Encoding.UTF8,
                                    "application/json"
                                );
                                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                                await client.PostAsync("https://localhost:7140/api/CarPricing/UpdateCarPricing", weekContent);
                            }

                            // Aylıq
                            if (perMonth != null)
                            {
                                var monthDto = new
                                {
                                    Id = perMonth.CarPricingId,
                                    CarId = carDto.id,
                                    PricingId = 4,
                                    Amount = carDto.PerMonthPrice,
                                    IsDelete = false
                                };

                                var monthContent = new StringContent(
                                    JsonConvert.SerializeObject(monthDto),
                                    Encoding.UTF8,
                                    "application/json"
                                );
                                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                                await client.PostAsync("https://localhost:7140/api/CarPricing/UpdateCarPricing", monthContent);
                            }
                        }

                        return RedirectToAction("Index");
                        
                    }
                }

            }
            return View(carDto);
        }

        public IActionResult ChangeLanguage(string lang)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                lang = "en";
            }
            Response.Cookies.Append("Language", lang);
            return Redirect(Request.GetTypedHeaders().Referer.ToString());
        }


        private async Task<string> UploadFileToAzure(IFormFile file, string containerName)
        {
            if (file == null || file.Length == 0)
                return null;

            try
            {
                // Azure Blob Storage müştərisini yaradırıq
                var blobServiceClient = new BlobServiceClient(connectionString);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
                await blobContainerClient.CreateIfNotExistsAsync(PublicAccessType.Blob); // Container yoxdursa, yaradır

                var blobClient = blobContainerClient.GetBlobClient(file.FileName);

                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, true); // Mövcud fayl varsa, üzərinə yazır
                }

                return blobClient.Uri.AbsoluteUri; // Yüklənmiş faylın URL-sini qaytarırıq
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                return null;
            }

        }
    }
}
