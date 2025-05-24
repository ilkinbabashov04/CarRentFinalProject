using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class BrandController : ControllerBase
	{
		private readonly IBrandService _brandService;
		public BrandController(IBrandService brandService)
		{
			_brandService = brandService;
		}
		[HttpPost("AddBrand")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(string brand)
		{
			var result = _brandService.Add(brand);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("UpdateBrand")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id, string brandName)
		{
			var result = _brandService.Update(id, brandName);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpDelete("DeleteBrand{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
		{
			var result = _brandService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetBrandById")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get(int id)
		{
			var result = _brandService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _brandService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
	}
}
