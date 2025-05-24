using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpPost("AddAuthor")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(Author author)
        {
            var result = _authorService.Add(author);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost("UpdateAuthor")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(Author author)
        {
            var result = _authorService.Update(author);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete("DeleteAuthor")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var result = _authorService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAuthorById")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get(int id)
        {
            var result = _authorService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _authorService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
