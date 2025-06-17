using System.ComponentModel.DataAnnotations;

namespace booklend.Models
{
    public class Bookstore
    {
        [Key]
        public Guid Id { get; set; }
        public string? BookstoreName { get; set; }

        // FK
        public Guid? AdminId { get; set; }

        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public User? Admin { get; set; }
        public ICollection<BookstoreBook>? BookstoreBooks { get; set; }

    }
}