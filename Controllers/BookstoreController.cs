using booklend.Application.DTOs.Bookstore;
using booklend.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace booklend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookstoreController(BookstoreService service) : ControllerBase
{
    private readonly BookstoreService _service = service;

    [HttpPost]
    public async Task<ActionResult<BookstoreReadDto>> Post([FromBody] BookstoreCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return created;
    }

    [HttpGet]
    public async Task<ActionResult<List<BookstoreReadDto>>> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }
    [HttpGet("getbyid/{id}")]
    public async Task<ActionResult<BookstoreReadDto>> GetById(Guid id)
    {
        var dto = await _service.GetByIdAsync(id);
        return Ok(dto);
    }

    [HttpGet("getbyname/{name}")]
    public async Task<ActionResult<BookstoreReadDto>> GetByName(string name)
    {
        var dto = await _service.GetByNameAsync(name);
        return Ok(dto);
    }
    [HttpPost("update/{id}")]
    public async Task<ActionResult<BookstoreReadDto>> Update(Guid id, [FromBody] BookstoreUpdateDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return Ok(updated);
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

}