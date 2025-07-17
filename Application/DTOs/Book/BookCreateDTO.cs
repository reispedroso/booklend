using System.ComponentModel.DataAnnotations;

namespace booklend.Application.DTOs.Book
{
    public class BookCreateDto
    {
        [Required]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Required]
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}