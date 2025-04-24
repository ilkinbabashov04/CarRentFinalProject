using Entities.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Ui.Helper;

namespace Ui.Controllers
{
    public class BlogController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BlogController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Blog";
            ViewBag.v2 = "Our Blog";
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7140/api/Blog/GetAllBlogsWithAuthor");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<AllBlogsWithAuthorDto>>>(jsonData);
                var values = apiResponse?.Data;
                return View(values);
            }

            return View();
        }

        public async Task<IActionResult> BlogDetail(int id)
        {
            ViewBag.v1 = "Blogs";
            ViewBag.v2 = "Blog Details and Comments";
            ViewBag.blogId = id;
            
            var client = _httpClientFactory.CreateClient();
            var responseMessage2 = await client.GetAsync($"https://localhost:7140/api/Comment/CommentCountByBlogId?id=" + id);
            var jsonData2 = await responseMessage2.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<CommentCountDto>>(jsonData2);
            var values2 = apiResponse?.Data;
            ViewBag.commentCount = values2.CommentCount;

            return View();
        }
        [HttpGet]
        public PartialViewResult AddComment(int id)
        {
            ViewBag.blogId = id;
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCommentDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7140/api/Comment/AddComment", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }
    }
}
