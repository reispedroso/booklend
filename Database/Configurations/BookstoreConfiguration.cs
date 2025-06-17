using booklend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace booklend.Database.Configurations
{
    public class BookstoreConfiguration : IEntityTypeConfiguration<Bookstore>
    {
        public void Configure(EntityTypeBuilder<Bookstore> builder)
        {
            builder
                .HasOne(b => b.Admin)
                .WithMany(u => u.BookstoresAdministered)
                .HasForeignKey(b => b.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasIndex(b => b.BookstoreName)
                .IsUnique();
        }
    }
}