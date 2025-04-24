using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarFeatureController : ControllerBase
    {
        private readonly ICarFeatureService _carFeatureService;
        public CarFeatureController(ICarFeatureService carFeatureService)
        {
            _carFeatureService = carFeatureService;
        }
        [HttpPost("AddCarFeature")]
        public IActionResult Add(CarFeature carFeature)
        {
            var result = _carFeatureService.Add(carFeature);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetCarFeatureByCarId")]
        public IActionResult GetCarFeatureByCarid(int id)
        {
            var result = _carFeatureService.GetCarFeatureByCarId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("CarFeatureChangeAvailableToFalse")]
        public IActionResult CarFeatureChangeAvailableToFalse(int id)
        {
            var result = _carFeatureService.CarFeatureChangeAvailableToFalse(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("CarFeatureChangeAvailableToTrue")]
        public IActionResult CarFeatureChangeAvailableToTrue(int id)
        {
            var result = _carFeatureService.CarFeatureChangeAvailableToTrue(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

    }
}
