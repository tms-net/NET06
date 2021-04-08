using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.NET06.BookingSystem
{
    interface IBookingRepository
    {
        IEnumerable<Service> GetServices();
        IEnumerable<BookEntry> GetBookingEntries(DateTime start, DateTime end, BookingStatus? status = null);

        int AddBookingEntry(int serviceId, int clientId, DateTime bookingDate);
    }
}
