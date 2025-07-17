using System.ComponentModel.DataAnnotations;
using booklend.Enums;

namespace booklend.Application.DTOs.BookItem
{
    public class BookItemCreateDto
    {
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public BookCondition Condition { get; set; }
    }
}