namespace booklend.Application.DTOs.Bookstore
{
    public class BookstoreReadDto
    {
        public Guid Id { get; set; }
        public string BookstoreName { get; set; } = default!;
        public string? City { get; set; }
        public string? State { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? AdminId { get; set; }
    }
}