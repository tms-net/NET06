using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.NET06.BookingSystem
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Service>> GetServicesAsync();
        Task<int> AddServiceAsync(Service service);

        Task<IEnumerable<Client>> GetClientsAsync();
        Task<Client> GetClientAsync(int clientId);
        
        Task<int> AddClientAsync(Client client);

        Task<IEnumerable<BookEntry>> GetBookingEntriesAsync(DateTime start, DateTime end, BookingStatus? status = null);
        
        Task<IEnumerable<BookEntry>> GetClientBookingsAsync(int clientId);
        
        Task<int> AddBookingEntryAsync(int serviceId, int clientId, DateTime bookingDate);

        void SaveEntry(BookEntry entry);
    }
}
