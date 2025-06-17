using System.ComponentModel.DataAnnotations;

namespace booklend.Application.DTOs.BookstoreBook
{
    public class BookstoreBookCreateDto
    {
        public Guid Id { get; set; }
        [Required]
        public Guid BookstoreId { get; set; }
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}