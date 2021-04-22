using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
using TMS.NET06.BookingSystem.Models;

namespace TMS.NET06.BookingSystem
{
    public class BookingContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<BookEntry> BookingEntries { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Client> Clients { get; internal set; }

        public BookingContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseInMemoryDatabase("BookingSystem");
            if (!string.IsNullOrEmpty(_connectionString))
                optionsBuilder.UseSqlServer(_connectionString);
            else
                optionsBuilder.UseInMemoryDatabase("BookingSystem");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>(
                service =>
                {
                    service.HasKey(c => c.ServiceId);
                });

            modelBuilder.Entity<BookEntry>(
                be =>
                {
                    be.HasKey(c => c.BookId);
                    be.Property(c => c.NotificationInfo)
                        .HasConversion(
                            n => JsonSerializer.Serialize(n, null),
                            val => JsonSerializer.Deserialize<NotificationInfo>(val, null));
                });

            modelBuilder.Entity<Client>(
                client =>
                {
                    client.HasKey(c => c.ClientId);
                    client.OwnsOne(c => c.ContactInformation);
                });
        }
    }
}
