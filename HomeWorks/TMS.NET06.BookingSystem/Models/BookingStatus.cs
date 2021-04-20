using System;

namespace TMS.NET06.BookingSystem
{
    [Flags]
    public enum BookingStatus
    {
        Undefined = 0,
        WaitingForConfirmation = 1,
        Confirmed = 2,
        Cancelled = 4,
        Failed = 8
    }
}