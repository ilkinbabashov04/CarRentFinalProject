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
    [Route("Admin/AdminTestimonial")]
    public class AdminTestimonialController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminTestimonialController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7140/api/Testimonial/GetAll");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<TestimonialDto>>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }

        [HttpGet]
        [Route("CreateTestimonial")]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        [HttpPost]
        [Route("CreateTestimonial")]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createTestimonialDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.PostAsync("https://localhost:7140/api/Testimonial/AddTestimonial", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminTestimonial", new { area = "Admin" });
                }
            }
            return View();
        }

        [Route("DeleteTestimonial/{id}")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.DeleteAsync($"https://localhost:7140/api/Testimonial/DeleteTestimonial?id={id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminTestimonial", new { area = "Admin" });
                }
            }
            return RedirectToAction("Index", "AdminTestimonial", new { area = "Admin" });
        }

        [HttpGet]
        [Route("UpdateTestimonial/{id}")]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync($"https://localhost:7140/api/Testimonial/GetTestimonialById?id={id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {jsonData}");
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<TestimonialDto>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }


        [HttpPost]
        [Route("UpdateTestimonial/{id}")]
        public async Task<IActionResult> UpdateTestimonial(TestimonialDto TestimonialDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(TestimonialDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.PostAsync("https://localhost:7140/api/Testimonial/UpdateTestimonial", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminTestimonial", new { area = "Admin" });
                }
            }
            return View();
        }
    }
}
