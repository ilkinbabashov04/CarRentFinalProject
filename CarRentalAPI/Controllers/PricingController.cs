using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class PricingController : ControllerBase
	{
		private readonly IPricingService _pricingService;
		public PricingController(IPricingService PricingService)
		{
			_pricingService = PricingService;
		}
		[HttpPost("AddPricing")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(Pricing pricing)
		{
			var result = _pricingService.Add(pricing);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("UpdatePricing")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(Pricing pricing)
		{
			var result = _pricingService.Update(pricing);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpDelete("DeletePricing")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
		{
			var result = _pricingService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetPricingById")]
		public IActionResult Get(int id)
		{
			var result = _pricingService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _pricingService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
        [HttpGet("GetPricingByCarId")]
        public IActionResult GetPricingByCarId(int id)
        {
            var result = _pricingService.GetPricingByCarId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
