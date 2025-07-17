using booklend.Application.DTOs.Rental;
using booklend.Application.DTOs.BookItem;
using booklend.Application.Services.Validation;
using booklend.Models;
using booklend.Repository.Interfaces;

namespace booklend.Application.Services;

public class RentalService(IRentalRepository rentalRep, BookItemService service)
{
    private readonly IRentalRepository _rentalRep = rentalRep;
    private readonly BookItemService _service = service;

    public async Task<RentalReadDto> CreateAsync(RentalCreateDTO dto, Guid userId)
    {
        RentalValidator.ValidateDate(dto.RentDate, dto.ReturnDate);

        var bookItem = await _service.GetByIdAsync(dto.BookItemId);
        if (bookItem.Quantity <= 0)
            throw new ArgumentException($"This bookstore is out of items, quantity now is of: {bookItem.Quantity}");

        bookItem.Quantity -= 1;

        var updateDto = new BookItemUpdateDto
        {
            Quantity = bookItem.Quantity,

        };
        await _service.UpdateAsync(dto.BookItemId, updateDto);


        var rentalEntity = new Rental
        {
            Id = Guid.NewGuid(),
            BookItemId = dto.BookItemId,
            UserId = userId,
            RentDate = DateTime.SpecifyKind(dto.RentDate, DateTimeKind.Utc),
            ReturnDate = DateTime.SpecifyKind(dto.ReturnDate, DateTimeKind.Utc),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            DeletedAt = null
        };

        await _rentalRep.CreateAsync(rentalEntity);
        return MapToDto(rentalEntity);
    }

    public async Task<IEnumerable<RentalReadDto>> GetAllAsync()
    {
        var entities = await _rentalRep.GetAllAsync()
        ?? throw new Exception("No rentals found!");

        return entities.Select(MapToDto);
    }

    public async Task<IEnumerable<RentalReadDto>> GetAllByUserIdAsync(Guid userId)
    {
        var entities = await _rentalRep.GetAllByUserIdAsync(userId)
        ?? throw new Exception("No rentals found for this user!");

        return entities.Select(MapToDto);
    }
    private static RentalReadDto MapToDto(Rental r) => new()
    {
        Id = r.Id,
        UserId = r.UserId,
        BookItemId = r.BookItemId
    };
}