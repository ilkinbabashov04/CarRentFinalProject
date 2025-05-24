using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;
        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }
        [HttpPost("AddTestimonial")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(Testimonial testimonial)
        {
            var result = _testimonialService.Add(testimonial);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("UpdateTestimonial")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(Testimonial testimonial)
        {
            var result = _testimonialService.Update(testimonial);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("DeleteTestimonial")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var result = _testimonialService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetTestimonialById")]
        public IActionResult Get(int id)
        {
            var result = _testimonialService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _testimonialService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
