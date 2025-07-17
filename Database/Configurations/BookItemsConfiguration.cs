using booklend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace booklend.Database.Configurations
{
    public class BookItemsConfiguration : IEntityTypeConfiguration<BookItem>
    {
        public void Configure(EntityTypeBuilder<BookItem> builder)
        {
            builder.ToTable("BookItems");

            builder.HasKey(bb => bb.Id);

            builder.HasOne(bb => bb.Bookstore)
                   .WithMany(bs => bs.BookItems)
                   .HasForeignKey(bb => bb.BookstoreId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bb => bb.Book)
                   .WithMany(b => b.BookItem)
                   .HasForeignKey(bb => bb.BookId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(bb => new { bb.BookstoreId, bb.BookId })
                   .IsUnique();

        }
    }
}
