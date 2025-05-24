using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		private readonly IHomeService _homeService;
		private readonly ILogger<HomeController> _logger;
		public HomeController(IHomeService homeService, ILogger<HomeController> logger)
		{
			_logger = logger;
			_homeService = homeService;
		}
		[HttpPost("AddHome")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(Home home)
		{
            _logger.LogInformation("AddHome method called at {Time} by user {User}", DateTime.Now, User.Identity?.Name ?? "Anonymous");
            var result = _homeService.Add(home);
			if (result.Success)
			{
                _logger.LogInformation("Home added successfully with ID {HomeId}", home.Id);
                return Ok(result);
			}
            _logger.LogWarning("Failed to add Home at {Time}", DateTime.Now);
            return BadRequest();
		}
		[HttpPost("UpdateHome")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(Home home)
		{
			var result = _homeService.Update(home);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpDelete("DeleteHome/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
		{
			var result = _homeService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetHomeById")]
		public IActionResult Get(int id)
		{
			var result = _homeService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _homeService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
	}
}
