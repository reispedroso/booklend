using booklend.Application.DTOs.BookRating;
using booklend.Application.Services;
using booklend.Controllers;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookRatingController(BookRatingService service) : ControllerBase
{
    private readonly BookRatingService _service = service;

    [HttpPost("/rate")]
    public async Task<ActionResult<BookRatingReadDto>> Post([FromBody] BookRatingCreateDto dto)
    {
        var created = await _service.RateBook(dto);

        return created;
    }
}