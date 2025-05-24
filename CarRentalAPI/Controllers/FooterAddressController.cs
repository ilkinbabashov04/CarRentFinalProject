using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class FooterAddressController : ControllerBase
	{
		private readonly IFooterAddressService _footerAddressService;
		public FooterAddressController(IFooterAddressService footerAddressService)
		{
			_footerAddressService = footerAddressService;
		}
		[HttpPost("AddFooterAddress")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(FooterAddress footerAddressService)
		{
			var result = _footerAddressService.Add(footerAddressService);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("UpdateFooterAddress")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(FooterAddress footerAddressService)
		{
			var result = _footerAddressService.Update(footerAddressService);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpDelete("DeleteFooterAddress")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
		{
			var result = _footerAddressService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetFooterAddressById")]
		public IActionResult Get(int id)
		{
			var result = _footerAddressService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _footerAddressService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
	}
}
