using Microsoft.AspNetCore.Mvc;
using booklend.Application.Services;
using booklend.Application.DTOs.Author;
using Microsoft.AspNetCore.Authorization;

namespace booklend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController(AuthorService service) : ControllerBase
    {
        private readonly AuthorService _service = service;

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AuthorReadDTO>> Post([FromBody] AuthorCreateDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return created;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorReadDTO>>> GetAll()
        {
            var list = await _service.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<AuthorReadDTO>> GetById(Guid id)
        {
            var dto = await _service.GetByIdAsync(id);
            return Ok(dto);
        }

    }
}