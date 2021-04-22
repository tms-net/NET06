using System;

namespace TMS.NET06.BookingSystem
{
    public class Service
    {
        public int ServiceId { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public TimeSpan Duration { get; set; }
    }
}