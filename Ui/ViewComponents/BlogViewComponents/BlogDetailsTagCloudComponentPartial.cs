using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ui.Helper;

namespace Ui.ViewComponents.BlogViewComponents
{
    public class BlogDetailsTagCloudComponentPartial : ViewComponent
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public BlogDetailsTagCloudComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.blogid = id;
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"https://localhost:7140/api/TagCloud/GetTagCloudsByBlogId?id=" + id);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<TagCloudDto>>>(jsonData);
                var values = apiResponse?.Data;
                return View(values);
            }

            return View();
        }
    }
}
