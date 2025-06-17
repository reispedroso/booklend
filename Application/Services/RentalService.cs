using booklend.Application.DTOs.Rental;
using booklend.Application.DTOs.BookstoreBook;
using booklend.Application.Services.Validation;
using booklend.Models;
using booklend.Repository.Interfaces;

namespace booklend.Application.Services;

public class RentalService(IRentalRepository rentalRep, BookstoreBookService service)
{
    private readonly IRentalRepository _rentalRep = rentalRep;
    private readonly BookstoreBookService _service = service;

    public async Task<RentalReadDto> CreateAsync(RentalCreateDTO dto)
    {
        RentalValidator.ValidateDate(dto.RentDate, dto.ReturnDate);

        var bookstoreBook = await _service.GetByIdAsync(dto.BookstoreBookId);
        if (bookstoreBook.Quantity <= 0)
            throw new ArgumentException($"This bookstore is out of items, quantity now is of: {bookstoreBook.Quantity}");

        bookstoreBook.Quantity -= 1;

        var updateDto = new BookstoreBookUpdateDto
        {
            Quantity = bookstoreBook.Quantity,

        };
        await _service.UpdateAsync(dto.BookstoreBookId, updateDto);

        var rentalEntity = new Rental
        {
            Id = Guid.NewGuid(),
            BookstoreBookId = dto.BookstoreBookId,
            UserId = dto.UserId,
            RentDate = DateTime.SpecifyKind(dto.RentDate, DateTimeKind.Utc),
            ReturnDate = DateTime.SpecifyKind(dto.ReturnDate, DateTimeKind.Utc),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = null,
            DeletedAt = null
        };

        await _rentalRep.CreateAsync(rentalEntity);
        return MapToDto(rentalEntity);
    }

    private static RentalReadDto MapToDto(Rental r) => new()
    {
        Id = r.Id,
        UserId = r.UserId,
        BookstoreBookId = r.BookstoreBookId
    };
}