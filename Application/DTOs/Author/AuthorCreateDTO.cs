using System.ComponentModel.DataAnnotations;

namespace booklend.Application.DTOs.Author
{
    public class AuthorCreateDto
    {
        [Required]
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Nationality { get; set; } = null!;

    }
}