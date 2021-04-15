using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> AddServiceAsync(Service service)
        {
            using (var context = CreateContext())
            {
                var result = await context.Services.AddAsync(service);
                return result.Entity.ServiceId;
            }
        }

        public int AddBookingEntry(int serviceId, int clientId, DateTime bookingDate)
        {
            throw new NotImplementedException();
        }

        public int AddClient(Client client)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookEntry> GetBookingEntries(DateTime start, DateTime end, BookingStatus? status = null)
        {
            throw new NotImplementedException();
        }

        public Client GetClient(int clientId)
        {
            using (var context = CreateContext())
            {
                return context.Clients.Find(clientId);
            }
        }

        public IEnumerable<BookEntry> GetClientBookings(int clientId)
        {
            using (var context = CreateContext())
            {
                return context.BookingEntries
                    .Where(be => be.Client.ClientId == clientId)
                    .ToArray();
            }
        }

        public IEnumerable<Client> GetClients()
        {
            using (var context = CreateContext())
            {
                return context.Clients.ToArray();
            }
        }

        public async Task<IEnumerable<Service>> GetServicesAsync()
        {
            using(var context = CreateContext())
            {
                return await context.Services.ToListAsync();
            }
        }

        public void SaveEntry(BookEntry entry)
        {
            throw new NotImplementedException();
        }

        private BookingContext CreateContext()
        {
            return new BookingContext(_configuration.GetConnectionString("BookingDb"));
        }
    }
}
