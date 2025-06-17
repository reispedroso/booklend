using System.ComponentModel.DataAnnotations;

namespace booklend.Application.DTOs.Book
{
    public class BookCreateDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        [Required]
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
    }
}