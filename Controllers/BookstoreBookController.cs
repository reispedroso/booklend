using Microsoft.AspNetCore.Mvc;
using booklend.Application.Services;
using booklend.Application.DTOs.BookstoreBook;

namespace booklend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookstoreBookController(BookstoreBookService service) : ControllerBase
{
    private readonly BookstoreBookService _service = service;

    [HttpPost]
    public async Task<ActionResult<BookstoreBookReadDto>> Post([FromBody] BookstoreBookCreateDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return created;
    }


}
