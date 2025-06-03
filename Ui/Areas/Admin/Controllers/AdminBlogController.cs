using Azure;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Ui.Helper;

namespace Ui.Areas.Admin.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/AdminBlog")]
    public class AdminBlogController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AdminBlogController(IHttpClientFactory httpClientFactory)
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
                var responseMessage = await client.GetAsync("https://localhost:7140/api/Blog/GetAllBlogsWithAuthor");
                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<ApiResponse<List<AllBlogsWithAuthorDto>>>(jsonData);
                    var values = apiResponse?.Data;
                    return View(values);
                }
            }
            return View();
        }

        [HttpGet]
        [Route("CreateBlog")]
        public async Task<IActionResult> CreateBlog()
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();

                // === Fetch Categories ===
                var categoryResponse = await client.GetAsync("https://localhost:7140/api/Category/GetAll");
                var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                var categoryApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<CategoryDto>>>(categoryJson);
                var categories = categoryApiResponse?.Data ?? new List<CategoryDto>();

                List<SelectListItem> categoryValues = categories.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
                ViewBag.CategoryValues = categoryValues;

                // === Fetch Authors ===
                var authorResponse = await client.GetAsync("https://localhost:7140/api/Author/GetAll");
                var authorJson = await authorResponse.Content.ReadAsStringAsync();
                var authorApiResponse = JsonConvert.DeserializeObject<ApiResponse<List<AuthorDto>>>(authorJson);
                var authors = authorApiResponse?.Data ?? new List<AuthorDto>();

                List<SelectListItem> authorValues = authors.Select(x => new SelectListItem
                {
                    Text = x.Name, // Adjust based on your DTO, could be x.Name or x.Username
                    Value = x.Id.ToString()
                }).ToList();
                ViewBag.AuthorValues = authorValues;
            }

            return View();
        }

        [HttpPost]
        [Route("CreateBlog")]
        public async Task<IActionResult> CreateBlog([FromForm] BlogFileDto createBlogDto, IFormFile CoverImage, IFormFile BigImage)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();

                using (var formData = new MultipartFormDataContent())
                {
                    // Add blog fields
                    formData.Add(new StringContent(createBlogDto.Title), "Title");
                    formData.Add(new StringContent(createBlogDto.Description), "Description");
                    formData.Add(new StringContent(createBlogDto.CategoryId.ToString()), "CategoryId");
                    formData.Add(new StringContent(createBlogDto.AuthorId.ToString()), "AuthorId");
                    
                    // Add CoverImage
                    if (CoverImage != null)
                    {
                        var coverImageContent = new StreamContent(CoverImage.OpenReadStream());
                        coverImageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(CoverImage.ContentType);
                        formData.Add(coverImageContent, "CoverImage", CoverImage.FileName);
                    }

                    if (BigImage != null)
                    {
                        var bigImageContent = new StreamContent(BigImage.OpenReadStream());
                        bigImageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(BigImage.ContentType);
                        formData.Add(bigImageContent, "BigImage", BigImage.FileName);
                    }

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    var responseMessage = await client.PostAsync("https://localhost:7140/api/Blog/AddBlog", formData);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }

        [Route("DeleteBlog/{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;
            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await client.DeleteAsync($"https://localhost:7140/api/Blog/DeleteBlog?id={id}");
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminBlog", new { area = "Admin" });
                }
            }
            return RedirectToAction("Index", "AdminBlog", new { area = "Admin" });
        }

        [HttpGet]
        [Route("AddTagCloud/{id}")]
        public IActionResult AddTagCloud(int id)
        {
            ViewBag.BlogId = id;
            return View();
        }


        [HttpPost]
        [Route("AddTagCloud/{id}")]
        public async Task<IActionResult> AddTagCloud(int id, TagCloudDto model)
        {
            var token = User.Claims.FirstOrDefault(x => x.Type == "accessToken")?.Value;

            if (token != null)
            {
                var client = _httpClientFactory.CreateClient();

                var tagData = new
                {
                    BlogId = id,
                    Title = model.Title
                };

                var jsonData = JsonConvert.SerializeObject(tagData);
                var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var response = await client.PostAsync("https://localhost:7140/api/TagCloud/AddTagCloud", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "AdminBlog", new { area = "Admin" });
                }
            }

            // Optionally: Add a ViewBag or TempData error message here.
            return RedirectToAction("Index", "AdminBlog", new { area = "Admin" });
        }

    }
}
