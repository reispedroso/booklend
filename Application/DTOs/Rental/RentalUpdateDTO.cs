namespace booklend.Application.DTOs.Rental
{
    public class RentalUpdateDto
    {
        public Guid BookItemId { get; set; }

        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}