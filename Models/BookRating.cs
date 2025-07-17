using System.ComponentModel.DataAnnotations;

namespace booklend.Models
{
    public class BookRating
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public decimal Note { get; set; }
        public Book? Book { get; set; }
    }

}