using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ui.Helper;

namespace Ui.ViewComponents.DashboardComponents
{
    public class AdminDashboardChart2ComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminDashboardChart2ComponentPartial(IHttpClientFactory httpClientFactory)
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
                var responseMessage = await client.GetAsync("https://localhost:7140/api/Car/GetPieChartDetail");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<PieChartDto>>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
                
            return View();
        }
    }
}
