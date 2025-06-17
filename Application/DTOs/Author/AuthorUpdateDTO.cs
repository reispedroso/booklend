namespace booklend.Application.DTOs.Author
{
    public class AuthorUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}