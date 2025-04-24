using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
