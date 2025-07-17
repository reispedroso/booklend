using System.ComponentModel.DataAnnotations;
using booklend.Enums;

namespace booklend.Models
{

    public class BookItem
    {
        [Key]
        public Guid Id { get; set; }

        public Guid BookstoreId { get; set; }
        public Guid BookId { get; set; }

        public int Quantity { get; set; }
        public BookCondition Condition { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public Bookstore? Bookstore { get; set; }
        public Book? Book { get; set; }
        public ICollection<Rental>? Rentals { get; set; }
    }
}