namespace booklend.Application.DTOs.Bookstore
{
    public class BookstoreCreateDto
    {
        public required string BookstoreName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
    }
}