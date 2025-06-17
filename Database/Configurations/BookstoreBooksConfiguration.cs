// Database/Configurations/BookstoreBookConfiguration.cs
using booklend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace booklend.Database.Configurations
{
    public class BookstoreBookConfiguration : IEntityTypeConfiguration<BookstoreBook>
    {
        public void Configure(EntityTypeBuilder<BookstoreBook> builder)
        {
            builder.ToTable("BookstoreBooks");

            // PK
            builder.HasKey(bb => bb.Id);

            // FK -> Bookstore
            builder.HasOne(bb => bb.Bookstore)
                   .WithMany(bs => bs.BookstoreBooks)
                   .HasForeignKey(bb => bb.BookstoreId)
                   .OnDelete(DeleteBehavior.Cascade);

            // FK -> Book
            builder.HasOne(bb => bb.Book)
                   .WithMany(b => b.BookstoreBooks)
                   .HasForeignKey(bb => bb.BookId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Índice único para impedir o mesmo livro duas vezes na mesma loja
            builder.HasIndex(bb => new { bb.BookstoreId, bb.BookId })
                   .IsUnique();

        }
    }
}
