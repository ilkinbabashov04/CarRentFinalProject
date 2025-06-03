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
    public class CarDescriptionController : ControllerBase
    {
        private readonly ICarDescriptionService _carDescriptionService;
        public CarDescriptionController(ICarDescriptionService carDescriptionService)
        {
            _carDescriptionService = carDescriptionService;
        }
        [HttpPost("AddCarDescription")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(CarDescription carDescription)
        {
            var result = _carDescriptionService.Add(carDescription);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("UpdateCarDescription")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Update(CarDescription carDescription)
        {
            var result = _carDescriptionService.Update(carDescription);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetCarDescriptionByCarId")]
        public IActionResult GetCarDescriptionByCarId(int id)
        {
            var result = _carDescriptionService.GetCarDescriptionByCarId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
