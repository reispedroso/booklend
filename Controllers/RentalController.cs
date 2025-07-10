using booklend.Application.DTOs.Rental;
using booklend.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace booklend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RentalController(RentalService service) : BaseController
{
    private readonly RentalService _service = service;

    [Authorize(Roles = "Admin, Client")]
    [HttpPost]
    public async Task<ActionResult<RentalReadDto>> Post([FromBody] RentalCreateDTO dto)
    {
        var userId = GetUserIdOrThrow();

        var created = await _service.CreateAsync(dto, userId);
        return created;
    }

    [HttpGet]
    public async Task<ActionResult<List<RentalReadDto>>> GetAllByUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null) return Unauthorized();

        var userId = Guid.Parse(userIdClaim.Value);

        var entities = await _service.GetAllByUserIdAsync(userId);
        return entities.ToList();
    }
}