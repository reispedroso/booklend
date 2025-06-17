namespace booklend.Application.DTOs.Book
{
    public class BookUpdateDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public Guid CategoryId { get; set; }
    }
}