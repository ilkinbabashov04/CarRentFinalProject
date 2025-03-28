using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AboutController : ControllerBase
	{
		private readonly IAboutService _aboutService;
        public AboutController(IAboutService aboutService)
        {
			_aboutService = aboutService;   
        }
        [HttpPost("AddAbout")]
		public IActionResult Add(About about)
		{
			var result = _aboutService.Add(about);
			if(result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("UpdateAbout")]
		public IActionResult Update(About about)
		{
			var result = _aboutService.Update(about);
			if(result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpDelete("DeleteAbout")]
		public IActionResult Delete(int id)
		{
			var result = _aboutService.Delete(id);
			if(result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetAboutById")]
		public IActionResult Get(int id)
		{
			var result = _aboutService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _aboutService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
	}
}
