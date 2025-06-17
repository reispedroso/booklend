using Microsoft.AspNetCore.Mvc;
using booklend.Application.Services;
using booklend.Application.DTOs.Category;
using Microsoft.AspNetCore.Authorization;

namespace booklend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(CategoryService service) : ControllerBase
{
    private readonly CategoryService _service = service;

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<CategoryReadDto>> Post([FromBody] CategoryCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return created;
    }
    [HttpGet]
    [Authorize(Roles = "Admin, Client")]
    public async Task<ActionResult<List<CategoryReadDto>>> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("getbyid/{id}")]
    [Authorize(Roles = "Admin, Client")]
    public async Task<ActionResult<CategoryReadDto>> GetById(Guid id)
    {
        var dto = await _service.GetByIdAsync(id);
        return Ok(dto);
    }

}
