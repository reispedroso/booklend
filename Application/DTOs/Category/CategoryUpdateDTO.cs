using System.ComponentModel.DataAnnotations;

namespace booklend.Application.DTOs.Category
{
    public class CategoryUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
