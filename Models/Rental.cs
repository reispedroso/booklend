using System.ComponentModel.DataAnnotations;

namespace booklend.Models
{
    public class Rental
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid BookItemId { get; set; }

        public DateTime? RentDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public User? User { get; set; }
        public BookItem? BookItem { get; set; }

    }
}