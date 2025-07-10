using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using booklend.Application.Services;
using booklend.Application.DTOs.BookstoreBook;

namespace booklend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookstoreBookController(BookstoreBookService service, BookstoreService bsService) : BaseController
{
    private readonly BookstoreBookService _service = service;
    private readonly BookstoreService _bsService = bsService;

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<BookstoreBookReadDto>> Post([FromBody] BookstoreBookCreateDto dto)
    {
        var userId = GetUserIdOrThrow();
        

        var created = await _service.CreateAsync(dto, userId);
        return created;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("update/{id}")]
    public async Task<ActionResult<BookstoreBookReadDto>> Update(
    Guid id,
    [FromBody] BookstoreBookUpdateDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        var bookstoreBook = await _service.GetByIdAsync(id);
        var bookstore = await _bsService.GetByIdAsync(bookstoreBook.BookstoreId);

        if (bookstore.AdminId != userId)
            return Forbid("You have no permission to updated this bookstore data");

        var updated = await _service.UpdateAsync(id, dto);
        return Ok(updated);
    }
}
