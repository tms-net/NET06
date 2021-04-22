using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TMS.NET06.BookingSystem
{
    public class EFBookingRepository : IBookingRepository
    {
        private readonly IConfiguration _configuration;

        public EFBookingRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task InitAsync()
        {
            using var context = CreateContext();
            await context.Database.EnsureCreatedAsync();
            await context.Database.MigrateAsync();
        }

        public async Task<int> AddServiceAsync(Service service)
        {
            using (var context = CreateContext())
            {
                var result = await context.Services.AddAsync(service);
                await context.SaveChangesAsync();
                return result.Entity.ServiceId;
            }
        }

        public async Task<int> AddBookingEntryAsync(int serviceId, int clientId, DateTime bookingDate)
        {
            await using var context = CreateContext();

            var searchClient = await context.Clients.FindAsync(clientId);
            var searchService = await context.Services.FindAsync(serviceId);

            var newBookEntry = new BookEntry {Client = searchClient, Service = searchService, VisitDate = bookingDate, Status = BookingStatus.WaitingForConfirmation, Comment = "Need much beer"};

            var result = await context.BookingEntries.AddAsync(newBookEntry);
            return result.Entity.BookId;
        }

        public async Task<int> AddClientAsync(Client client)
        {
            await using var context = CreateContext();
            var result = await context.Clients.AddAsync(client);
            return result.Entity.ClientId;
        }

        public async Task<IEnumerable<BookEntry>> GetBookingEntriesAsync(DateTime start, DateTime end, BookingStatus? status = null)
        {
            await using var context = CreateContext();
            return await context.BookingEntries
                .Where(s => ((s.VisitDate > start) && (s.VisitDate < end) && (s.Status == status)))
                .ToListAsync();
        }

        public async Task<Client> GetClientAsync(int clientId)
        {
            await using var context = CreateContext();
            return await context.Clients.FindAsync(clientId);
        }

        public async Task<IEnumerable<BookEntry>> GetClientBookingsAsync(int clientId)
        {
            await using (var context = CreateContext())
            {
                return await context.BookingEntries
                    .Where(be => be.Client.ClientId == clientId)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Client>> GetClientsAsync()
        {
            await using var context = CreateContext();
            return await context.Clients.ToListAsync();
        }

        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            await using var context = CreateContext();
            return await context.Services.ToListAsync();
        }

        public async Task<bool> SaveEntryAsync(BookEntry entry)
        {
            await using (var context = CreateContext())
            {
                if (entry == null) return false;
                //context.BookingEntries.Attach(entry);
                context.Entry(entry).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<BookEntry> GetBookingAsync(int bookingid)
        {
            await using var context = CreateContext();
            return await context.BookingEntries.FindAsync(bookingid);
        }

        public async Task<Service> GetServiceAsync(int serviceId)
        {
            await using var context = CreateContext();
            return await context.FindAsync<Service>(serviceId);
        }

        private BookingContext CreateContext()
        {
            return new BookingContext(_configuration.GetConnectionString("BookingDb"));
        }
    }
}
