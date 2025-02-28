using Business.Abstract;
using Entities.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
		private readonly ICarService _carService;
		public CarController(ICarService carService)
		{
			_carService = carService;
		}
		[HttpPost("AddCar")]
		public IActionResult Add(Car car)
		{
			var result = _carService.Add(car);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("UpdateCar {id}")]
		public IActionResult Update(Car car)
		{
			var result = _carService.Update(car);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpDelete("DeleteCar {id}")]
		public IActionResult Delete(int id)
		{
			var result = _carService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetCarById {id}")]
		public IActionResult Get(int id)
		{
			var result = _carService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _carService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}

        [HttpGet("Get5Cars")]
        public IActionResult GetFiveCars()
        {
            var result = _carService.GetFiveCars();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
