using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        [HttpGet("GetCarCount")]
        public IActionResult GetCarCount()
        {
            var result = _statisticsService.GetCarCount();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAuthorCount")]
        public IActionResult GetAuthorCount()
        {
            var result = _statisticsService.GetAuthorCount();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetLocationCount")]
        public IActionResult GetLocationCount()
        {
            var result = _statisticsService.GetLocationCount();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetBlogCount")]
        public IActionResult GetBlogCount()
        {
            var result = _statisticsService.GetBlogCount();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetBrandCount")]
        public IActionResult GetBrandCount()
        {
            var result = _statisticsService.GetBrandCount();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAvrRentPriceForDaily")]
        public IActionResult GetAvrRentPriceForDaily()
        {
            var result = _statisticsService.GetAvrRentPriceForDaily();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAvrRentPriceForWeekly")]
        public IActionResult GetAvrRentPriceForWeekly()
        {
            var result = _statisticsService.GetAvrRentPriceForWeekly();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAvrRentPriceForMonthly")]
        public IActionResult GetAvrRentPriceForMonthly()
        {
            var result = _statisticsService.GetAvrRentPriceForMonthly();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetCarCountByTransmissionIsAuto")]
        public IActionResult GetCarCountByTransmissionIsAuto()
        {
            var result = _statisticsService.GetCarCountByTransmissionIsAuto();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetCarCountByKmLessThan1000")]
        public IActionResult GetCarCountByKmLessThan1000()
        {
            var result = _statisticsService.GetCarCountByKmLessThan1000();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetCarCountByFuelGasolineOrDiesel")]
        public IActionResult GetCarCountByFuelGasolineOrDiesel()
        {
            var result = _statisticsService.GetCarCountByFuelGasolineOrDiesel();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetCarCountByFuelElectric")]
        public IActionResult GetCarCountByFuelElectric()
        {
            var result = _statisticsService.GetCarCountByFuelElectric();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetCarBrandAndModelByDailyRentPriceIsMax")]
        public IActionResult GetCarBrandAndModelByDailyRentPriceIsMax()
        {
            var result = _statisticsService.GetCarBrandAndModelByDailyRentPriceIsMax();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetCarBrandAndModelByDailyRentPriceIsMin")]
        public IActionResult GetCarBrandAndModelByDailyRentPriceIsMin()
        {
            var result = _statisticsService.GetCarBrandAndModelByDailyRentPriceIsMin();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetBrandNameByMaxCar")]
        public IActionResult GetBrandNameByMaxCar()
        {
            var result = _statisticsService.GetBrandNameByMaxCar();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("BlogTitleByMaxBlogComment")]
        public IActionResult BlogTitleByMaxBlogComment()
        {
            var result = _statisticsService.BlogTitleByMaxBlogComment();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
