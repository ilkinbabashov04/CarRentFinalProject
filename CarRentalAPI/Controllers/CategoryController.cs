using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _categoryService;
		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		[HttpPost("AddCategory")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(Category category)
		{
			var result = _categoryService.Add(category);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("UpdateCategory")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(Category category)
		{
			var result = _categoryService.Update(category);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpDelete("DeleteCategory")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
		{
			var result = _categoryService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetCategoryById")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get(int id)
		{
			var result = _categoryService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _categoryService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
	}
}
