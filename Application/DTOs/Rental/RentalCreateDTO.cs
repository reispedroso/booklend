namespace booklend.Application.DTOs.Rental
{
    public class RentalCreateDTO
    {
        public Guid UserId { get; set; }
        public Guid BookItemId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}