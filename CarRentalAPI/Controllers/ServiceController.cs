using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ServiceController : ControllerBase
	{
		private readonly IServiceService _serviceService;
		public ServiceController(IServiceService serviceService)
		{
			_serviceService = serviceService;
		}
		[HttpPost("AddService")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(Service service)
		{
			var result = _serviceService.Add(service);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("UpdateService")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(Service service)
		{
			var result = _serviceService.Update(service);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpDelete("DeleteService")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
		{
			var result = _serviceService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetServiceById")]
		public IActionResult Get(int id)
		{
			var result = _serviceService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _serviceService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
	}
}
