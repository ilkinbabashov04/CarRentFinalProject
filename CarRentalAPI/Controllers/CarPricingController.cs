using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
	}
}
