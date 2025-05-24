using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Ui.Helper;

namespace Ui.Controllers
{
    public class AdminRolesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminRolesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync("https://localhost:7140/api/Role/GetAll");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<RolesDto>>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }

        public async Task<IActionResult> DeleteRole(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.DeleteAsync($"https://localhost:7140/api/Role/DeleteRole {id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateRole(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync($"https://localhost:7140/api/Role/GetRoleById {id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {jsonData}");
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<RolesDto>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UpdateRole(UpdateUserRoleDto rolesDto)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(rolesDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.PutAsync($"https://localhost:7140/api/AppUser/UpdateUserRole/{rolesDto.AppUserId}", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View();
        }


    }
}
