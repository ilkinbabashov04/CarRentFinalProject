using Business.Abstract;
using Entities.Concrete;
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
