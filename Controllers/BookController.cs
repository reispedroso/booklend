using booklend.Application.Services;
using booklend.Application.DTOs.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using booklend.Models;


namespace booklend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController(BookService service) : ControllerBase
    {
        private readonly BookService _service = service;

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookReadDto>> Post([FromBody] BookCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return created;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Client")]
        public async Task<ActionResult<List<BookReadDto>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("getbyid/{id}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<ActionResult<BookReadDto>> GetById(Guid id)
        {
            var dto = await _service.GetByIdAsync(id);
            return Ok(dto);
        }
        [HttpGet("getbyname/{name}")]
        [Authorize(Roles = "Admin,Client")]
        public async Task<ActionResult<BookReadDto>> GetByName(string name)
        {
            var dto = await _service.GetByNameAsync(name);
            return Ok(dto);
        }

        [HttpPost("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BookReadDto>> Update(Guid id, [FromBody] BookUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return Ok(updated);
        }

        [HttpPost("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}