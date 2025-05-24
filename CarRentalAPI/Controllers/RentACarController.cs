using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentACarController : ControllerBase
    {
        private readonly IRentACarService _rentACarService;
        public RentACarController(IRentACarService rentACarService)
        {
            _rentACarService = rentACarService;
        }
        [HttpGet("GetByFilter{id}/{available}")]
        public IActionResult GetByFilter(int id, bool available)
        {
            var result = _rentACarService.GetByFilter(id, available);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetBarGraphDetail")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetBarGraphDetail()
        {
            var result = _rentACarService.GetBarGraphDetail();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        
    }
}
