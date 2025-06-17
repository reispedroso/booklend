namespace booklend.Application.DTOs.BookstoreBook
{
    public class BookstoreBookReadDto
    {
        public Guid Id { get; set; }
        public Guid BookstoreId { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}