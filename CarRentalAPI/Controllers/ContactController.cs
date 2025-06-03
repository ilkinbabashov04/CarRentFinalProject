using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ContactController : ControllerBase
	{
		private readonly IContactService _contactService;
		public ContactController(IContactService contactService)
		{
			_contactService = contactService;
		}
		[HttpPost("AddContact")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Add(Contact contact)
		{
			var result = _contactService.Add(contact);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("UpdateContact")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Update(Contact contact)
		{
			var result = _contactService.Update(contact);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpPost("DeleteContact")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
		{
			var result = _contactService.Delete(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetContactById")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Get(int id)
		{
			var result = _contactService.GetById(id);
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
		[HttpGet("GetAll")]
		public IActionResult GetAll()
		{
			var result = _contactService.GetAll();
			if (result.Success)
			{
				return Ok(result);
			}
			return BadRequest();
		}
	}
}
