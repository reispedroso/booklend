using Microsoft.AspNetCore.Mvc;
using booklend.Application.DTOs.User;
using booklend.Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace booklend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService service) : ControllerBase
{
    private readonly UserService _service = service;

    [HttpPost]
    public async Task<ActionResult<UserReadDto>> Post([FromBody] UserCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return created;
    }

    [Authorize(Roles = "Admin, Client")]
    [HttpGet]
    public async Task<ActionResult<List<UserReadDto>>> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    [Authorize(Roles = "Admin, Client")]
    [HttpGet("getbyid/{id}")]
    public async Task<ActionResult<UserReadDto>> GetById(Guid id)
    {
        var dto = await _service.GetByIdAsync(id);
        return Ok(dto);
    }

    [Authorize(Roles = "Admin, Client")]
    [HttpPost("update/{id}")]
    public async Task<ActionResult<UserReadDto>> Update(Guid id, [FromBody] UserUpdateDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return Ok(updated);
    }

    [Authorize(Roles = "Admin, Client")]
    [HttpPost("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
