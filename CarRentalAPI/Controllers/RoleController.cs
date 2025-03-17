using Business.Abstract;
using Core.Entity.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        //[HttpPost("UpdateRole")]
        //public IActionResult Update(AppRole role)
        //{
        //    var result = _roleService.Update(role);
        //    if (result.Success)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest();
        //}
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _roleService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("DeleteRole {id}")]
        public IActionResult Delete(int id)
        {
            var result = _roleService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetRoleById {id}")]
        public IActionResult Get(int id)
        {
            var result = _roleService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
