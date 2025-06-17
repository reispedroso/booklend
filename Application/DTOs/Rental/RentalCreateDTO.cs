namespace booklend.Application.DTOs.Rental
{
    public class RentalCreateDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookstoreBookId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

    }
}