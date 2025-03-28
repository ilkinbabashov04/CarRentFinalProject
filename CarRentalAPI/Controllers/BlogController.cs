using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpPost("AddBlog")]
        public IActionResult Add(Blog blog)
        {
            var result = _blogService.Add(blog);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("UpdateBlog")]
        public IActionResult Update(Blog blog)
        {
            var result = _blogService.Update(blog);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("DeleteBlog")]
        public IActionResult Delete(int id)
        {
            var result = _blogService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetBlogById")]
        public IActionResult Get(int id)
        {
            var result = _blogService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _blogService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("Get3Blogs")]
        public IActionResult GetThreeBlogs()
        {
            var result = _blogService.GetLastThreeBlogs();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAllBlogsWithAuthor")]
        public IActionResult GetAllBlogsWithAuthor()
        {
            var result = _blogService.GetAllBlogsWithAuthor();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetBlogsByAuthorId")]
        public IActionResult GetBlogsByAuthorId(int id)
        {
            var result = _blogService.GetBlogWithoutAuthorId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
