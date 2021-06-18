using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStoreKhaled.Models
{
    public class StoreBooksDbContext : DbContext
    {
        public StoreBooksDbContext(DbContextOptions<StoreBooksDbContext> options):base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }
    }
}
