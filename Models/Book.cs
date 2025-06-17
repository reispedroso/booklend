using System.ComponentModel.DataAnnotations;

namespace booklend.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        // Navegação
        public Author? Author { get; set; }
        public Category? Category { get; set; }
        public ICollection<BookstoreBook>? BookstoreBooks { get; set; }

    }
}