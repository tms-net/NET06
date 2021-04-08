using Microsoft.EntityFrameworkCore;

namespace TMS.NET06.BookingSystem
{
    internal class BookingContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<BookEntry> BookingEntries { get; set; }

        public BookingContext(string connectionString)
        {
            this._connectionString = connectionString;
        }
    }
}
