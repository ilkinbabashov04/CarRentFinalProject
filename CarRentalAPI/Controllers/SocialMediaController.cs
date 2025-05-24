using Business.Abstract;
//using Business.BusinessAspect.Autofac.Secured;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;

//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class SocialMediaController : ControllerBase
	{
		private readonly ISocialMediaService _socialMediaService;
		
		public SocialMediaController(ISocialMediaService socialMediaService)
		{
			_socialMediaService = socialMediaService;
		}
        [HttpPost("AddSocialMedia")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(SocialMedia socialMedia)
		{
			var result = _socialMediaService.Add(socialMedia);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("UpdateSocialMedia")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(SocialMedia socialMedia)
		{
			var result = _socialMediaService.Update(socialMedia);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpDelete("DeleteSocialMedia")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
		{
			var result = _socialMediaService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetSocialMediaById")]
		public IActionResult Get(int id)
		{
			var result = _socialMediaService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _socialMediaService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
	}
}
