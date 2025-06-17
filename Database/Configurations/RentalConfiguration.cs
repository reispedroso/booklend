using booklend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace booklend.Database.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder
            .HasOne(r => r.User)
            .WithMany(u => u.Rentals)
            .HasForeignKey(r => r.UserId);

            builder
             .HasOne(r => r.BookstoreBook)
             .WithMany(bb => bb.Rentals)
            .HasForeignKey(r => r.BookstoreBookId);
        }
    }
}