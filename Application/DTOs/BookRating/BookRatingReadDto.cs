namespace booklend.Application.DTOs.BookRating
{
    public class BookRatingReadDto
    {
         public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public decimal Note { get; set;  }
    }
}