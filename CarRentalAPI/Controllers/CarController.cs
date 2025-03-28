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
        private readonly IFileService _fileService;

        public CarController(ICarService carService, IFileService fileService)
        {
            _carService = carService;
            _fileService = fileService;
        }
        [HttpPost("AddCar")]
        public async Task<IActionResult> Add([FromForm] CarFileDto carDto, IFormFile coverImage, IFormFile bigImage)
        {
            var car = new Car
            {
                BrandId = carDto.BrandId,
                Model = carDto.Model,
                Km = carDto.Km,
                Transmission = carDto.Transmission,
                Seat = carDto.Seat,
                Luggage = carDto.Luggage,
                Fuel = carDto.Fuel
            };

            if (coverImage != null)
            {
                car.CoverImageUrl = await _fileService.UploadImageToAzure(coverImage);
            }
            if (bigImage != null)
            {
                car.BigImageUrl = await _fileService.UploadImageToAzure(bigImage);
            }

            var result = _carService.Add(car);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("UpdateCar/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] CarFileDto carDto, IFormFile? coverImage, IFormFile? bigImage)
        {
            var existingCar = _carService.GetById(id);
            if (!existingCar.Success)
            {
                return NotFound("Car not found.");
            }

            var car = existingCar.Data;
            car.BrandId = carDto.BrandId;
            car.Model = carDto.Model;
            car.Km = carDto.Km;
            car.Transmission = carDto.Transmission;
            car.Seat = carDto.Seat;
            car.Luggage = carDto.Luggage;
            car.Fuel = carDto.Fuel;

            // Əgər yeni şəkil yüklənərsə, köhnəsini silib yenisini əlavə et
            if (coverImage != null)
            {
                await _fileService.DeleteImageFromAzure(car.CoverImageUrl); // Köhnəni sil
                car.CoverImageUrl = await _fileService.UploadImageToAzure(coverImage); // Yenini əlavə et
            }

            if (bigImage != null)
            {
                await _fileService.DeleteImageFromAzure(car.BigImageUrl);
                car.BigImageUrl = await _fileService.UploadImageToAzure(bigImage);
            }

            var result = _carService.Update(car);
            return result.Success ? Ok(result) : BadRequest(result);
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
