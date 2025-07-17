using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using booklend.Application.Services;
using booklend.Application.DTOs.BookItem;

namespace booklend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookItemController(BookItemService service, BookstoreService bsService) : BaseController
{
    private readonly BookItemService _service = service;
    private readonly BookstoreService _bsService = bsService;

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<BookItemReadDto>> Post([FromBody] BookItemCreateDto dto)
    {
        var userId = GetUserIdOrThrow();
        

        var created = await _service.CreateAsync(dto, userId);
        return created;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("update/{id}")]
    public async Task<ActionResult<BookItemReadDto>> Update(
    Guid id,
    [FromBody] BookItemUpdateDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        var bookItem = await _service.GetByIdAsync(id);
        var bookstore = await _bsService.GetByIdAsync(bookItem.BookstoreId);

        if (bookstore.AdminId != userId)
            return Forbid("You have no permission to updated this bookstore data");

        var updated = await _service.UpdateAsync(id, dto);
        return Ok(updated);
    }
}
