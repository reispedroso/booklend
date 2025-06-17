using booklend.Application.DTOs.Rental;
using booklend.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace booklend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RentalController(RentalService service) : ControllerBase
{
    private readonly RentalService _service = service;

    [Authorize(Roles = "Admin, Client")]
    [HttpPost]
    public async Task<ActionResult<RentalReadDto>> Post([FromBody] RentalCreateDTO dto)
    {
        var created = await _service.CreateAsync(dto);
        return created;
    }
}