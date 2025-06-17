using System.ComponentModel.DataAnnotations;

namespace booklend.Application.DTOs.Category
{
    public class CategoryCreateDto
    {
        [Required]
        public string? Name { get; set; } = string.Empty;
    }
}