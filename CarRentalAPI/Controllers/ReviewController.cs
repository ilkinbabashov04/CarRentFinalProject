using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : ControllerBase
	{
		private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
		[HttpPost("AddReview")]
		public IActionResult Add(Review review)
		{
			var result = _reviewService.Add(review);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("UpdateReview")]
		public IActionResult Update(Review review)
		{
			var result = _reviewService.Update(review);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetById")]
		public IActionResult GetById(int id)
		{
			var result = _reviewService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetReviewsByCarId")]
		public IActionResult GetReviewsByCarId(int id)
		{
			var result = _reviewService.GetReviewsByCarId(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
	}
}
