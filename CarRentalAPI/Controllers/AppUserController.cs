using Business.Abstract;
using Core.Entity.Concrete;
using Entities.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _appUserService;
        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }
        [HttpPut("UpdateUserRole/{userId}")]
        public IActionResult UpdateUserRole(int userId, [FromBody] UpdateUserRoleDto updateUserRoleDto)
        {
            if (userId != updateUserRoleDto.AppUserId)
            {
                return BadRequest("UserId mismatch!");
            }

            var result = _appUserService.UpdateUserRole(userId, updateUserRoleDto.RoleId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
