using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.NET06.BookingSystem
{
    public interface IBookingRepository
    {
        IEnumerable<Service> GetServices();
        
        IEnumerable<Client> GetClients();
        Client GetClient(int clientId);
        int AddClient(Client client);

        IEnumerable<BookEntry> GetBookingEntries(DateTime start, DateTime end, BookingStatus? status = null);
        IEnumerable<BookEntry> GetClientBookings(int clientId);
        int AddBookingEntry(int serviceId, int clientId, DateTime bookingDate);
        void SaveEntry(BookEntry entry);
    }
}
