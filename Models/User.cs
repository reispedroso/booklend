using System.ComponentModel.DataAnnotations;

namespace booklend.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }

    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; } 
    public string? Password { get; set; }
    // FK 
    public Guid RoleId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public Role? Role { get; set; }
    public ICollection<Bookstore>? BookstoresAdministered { get; set; }
    public ICollection<Rental>? Rentals { get; set; }
}


