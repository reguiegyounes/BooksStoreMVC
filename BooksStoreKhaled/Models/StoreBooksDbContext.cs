using Microsoft.EntityFrameworkCore;

namespace BooksStoreKhaled.Models
{
    public class StoreBooksDbContext : DbContext
    {
        public StoreBooksDbContext(DbContextOptions<StoreBooksDbContext> options):base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
