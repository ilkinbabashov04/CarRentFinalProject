using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarPricingController : ControllerBase
    {
        private readonly ICarPricingService _carPricingService;

        public CarPricingController(ICarPricingService carPricingService)
        {
            _carPricingService = carPricingService;
        }
        [HttpPost("AddCarPricing")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(CarPricing carPricing)
        {
            var result = _carPricingService.Add(carPricing);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("UpdateCarPricing")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(CarPricing carPricing)
        {
            var result = _carPricingService.Update(carPricing);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _carPricingService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
		[HttpGet("GetCarPricingWithTimePeriods")]
		public IActionResult GetCarPricingWithTimePeriods()
		{
			var result = _carPricingService.GetCarPricingWithTimePeriods();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
        [HttpGet("GetCarPricingByCarId")]
        public IActionResult GetCarPricingByCarId(int id)
        {
            var result = _carPricingService.GetCarPricingByCarId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
