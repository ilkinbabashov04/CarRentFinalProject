using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FeatureController : ControllerBase
	{
		private readonly IFeatureService _featureService;
		public FeatureController(IFeatureService featureService)
		{
			_featureService = featureService;
		}
		[HttpPost("AddFeature")]
		public IActionResult Add(Feature feature)
		{
			var result = _featureService.Add(feature);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("UpdateFeature")]
		public IActionResult Update(Feature feature)
		{
			var result = _featureService.Update(feature);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("DeleteFeature")]
		public IActionResult Delete(int id)
		{
			var result = _featureService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetFeatureById")]
		public IActionResult Get(int id)
		{
			var result = _featureService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _featureService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
	}
}
