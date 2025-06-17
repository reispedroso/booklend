using System.ComponentModel.DataAnnotations;

namespace booklend.Models
{

    public class BookstoreBook
    {
        [Key]
        public Guid Id { get; set; }

        // FKs
        public Guid BookstoreId { get; set; }
        public Guid BookId { get; set; }


        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Entidades de navegação
        public Bookstore? Bookstore { get; set; }
        public Book? Book { get; set; }
        public ICollection<Rental>? Rentals { get; set; }
    }
}