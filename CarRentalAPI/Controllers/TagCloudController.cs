using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagCloudController : ControllerBase
    {
        private readonly ITagCloudService _tagCloudService;
        public TagCloudController(ITagCloudService tagCloudService)
        {
            _tagCloudService = tagCloudService;
        }
        [HttpPost("AddTagCloud")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(TagCloud tagCloud)
        {
            var result = _tagCloudService.Add(tagCloud);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("UpdateTagCloud")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(TagCloud tagCloud)
        {
            var result = _tagCloudService.Update(tagCloud);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("DeleteTagCloud")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var result = _tagCloudService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetTagCloudById")]
        public IActionResult Get(int id)
        {
            var result = _tagCloudService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _tagCloudService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetTagCloudsByBlogId")]
        public IActionResult GetTagCloudsByBlogId(int id)
        {
            var result = _tagCloudService.GetTagCloudsByBlogId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
