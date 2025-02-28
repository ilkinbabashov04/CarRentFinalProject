using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ui.Helper;

namespace Ui.ViewComponents.BlogViewComponents
{
    public class BlogDetailsCategoryComponentPartial : ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BlogDetailsCategoryComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7140/api/Category/GetAll");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<CategoryDto>>>(jsonData);
                var values = apiResponse?.Data;
                return View(values);
            }

            return View();
        }
    }
}
