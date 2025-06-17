using Microsoft.AspNetCore.Mvc;
using booklend.Application.Services;
using booklend.Application.DTOs.BookstoreBook;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace booklend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookstoreBookController(BookstoreBookService service) : ControllerBase
{
    private readonly BookstoreBookService _service = service;

    [Authorize(Roles = "Admin, Client")]
    [HttpPost]
    public async Task<ActionResult<BookstoreBookReadDto>> Post([FromBody] BookstoreBookCreateDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        var created = await _service.CreateAsync(dto, userId);
        return created;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("update/{id}")]
    public async Task<ActionResult<BookstoreBookReadDto>> Update(
    Guid id,
    [FromBody] BookstoreBookUpdateDto dto)
   {
    var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    var updated = await _service.UpdateAsync(id, userId, dto);
    return Ok(updated);
}
}
