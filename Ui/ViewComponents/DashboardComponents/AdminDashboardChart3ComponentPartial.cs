using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ui.Helper;

namespace Ui.ViewComponents.DashboardComponents
{
    public class AdminDashboardChart3ComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminDashboardChart3ComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var token = UserClaimsPrincipal.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.GetAsync("https://localhost:7140/api/RentACar/GetBarGraphDetail");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<BarGraphDto>>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }
    }
}
