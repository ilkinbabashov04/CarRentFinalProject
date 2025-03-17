using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersRoleIdController : ControllerBase
    {
        private readonly IAppUsersRoleIdService _appUsersRoleIdService;
        public AppUsersRoleIdController(IAppUsersRoleIdService appUsersRoleIdService)
        {
            _appUsersRoleIdService = appUsersRoleIdService;
        }
        
        [HttpGet("GetAppUsersRoleIdById")]
        public IActionResult Get(int id)
        {
            var result = _appUsersRoleIdService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
