using System;

namespace TMS.NET06.BookingSystem
{
    public class BookEntry
    {
        public int BookId { get; set; }

        public string Comment { get; set; }

        public Service Service { get; set; }

        public DateTime VisitDate { get; set; }

        public BookingStatus Status { get; set; }

        public Client Client { get; set; }
    }
}
