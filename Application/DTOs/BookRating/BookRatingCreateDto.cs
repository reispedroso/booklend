namespace booklend.Application.DTOs.BookRating
{
    public class BookRatingCreateDto
    {
        public Guid BookId { get; set; }
        public decimal Note { get; set;  }
        public Models.Book? Book { get; set; }
        
    }
}