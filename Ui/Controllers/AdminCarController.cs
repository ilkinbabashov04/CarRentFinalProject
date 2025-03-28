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

namespace Ui.Controllers
{
    public class AdminCarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string connectionString = "DefaultEndpointsProtocol=https;AccountName=filestorageilkinbabashov;AccountKey=JGt7ng+bUaS3nc2Q+v2izbdjkpqOXf4tWeQpcLD+xfIeiDd5jtSMXcmG/V6aR+SrDkF6kbuOlyyh+AStEjuRRA==;EndpointSuffix=core.windows.net";
        private readonly string shareName = "cars";
        public AdminCarController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
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
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateCar()
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCar(CarFileDto createCarDto, IFormFile BigImage, IFormFile CoverImage)
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

                var responseMessage = await client.PostAsync("https://localhost:7140/api/Car/AddCar", formData);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }




        public async Task<IActionResult> DeleteCar(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync($"https://localhost:7140/api/Car/DeleteCar {id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCar(int id)
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


        [HttpPost]
        public async Task<IActionResult> UpdateCar(CarDto carDto, IFormFile? CoverImage, IFormFile? BigImage)
        {
            var client = _httpClientFactory.CreateClient();

            // Fetch Brand Name
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

            // Send request
            var responseMessage = await client.PostAsync($"https://localhost:7140/api/Car/UpdateCar/{carDto.id}", formData);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
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
