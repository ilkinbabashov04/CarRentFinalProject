using Business.Abstract;
//using Business.BusinessAspect.Autofac.Secured;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
	[ApiController]
    public class LocationController : ControllerBase
	{
		private readonly ILocationService _locationService;
		public LocationController(ILocationService locationService)
		{
			_locationService = locationService;
		}
		[HttpPost("AddLocation")]
		public IActionResult Add(Location location)
		{
			var result = _locationService.Add(location);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("UpdateLocation")]
		public IActionResult Update(Location location)
		{
			var result = _locationService.Update(location);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("DeleteLocation")]
		public IActionResult Delete(int id)
		{
			var result = _locationService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetLocationById")]
		public IActionResult Get(int id)
		{
			var result = _locationService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
        [HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _locationService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
	}
}
