using Lab4.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data
{
    public class DbConnection : DbContext
    {
        public DbConnection(DbContextOptions<DbConnection> options) : base(options)
        {

        }

        public DbSet<Reader> Readers { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;
        public DbSet<InfoBook> InfoBooks { get; set; } = null!;
    }
}
