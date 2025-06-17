using Microsoft.EntityFrameworkCore;
using booklend.Models;
using booklend.Database.Configurations;

namespace booklend.Database
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookstoreBook> BookstoreBooks { get; set; }
        public DbSet<Bookstore> Bookstores { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RentalConfiguration());
            modelBuilder.ApplyConfiguration(new BookstoreConfiguration());
            modelBuilder.ApplyConfiguration(new BookstoreBookConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());

        }
    }
}