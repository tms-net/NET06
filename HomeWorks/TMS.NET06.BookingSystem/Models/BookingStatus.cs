using System;

namespace TMS.NET06.BookingSystem
{
    [Flags]
    public enum BookingStatus
    {
        Undefined = 0b0000_0000,
        WaitingForConfirmation = 0b0000_0001,
        Confirmed = 0b0000_0010,
        Cancelled = 0b0000_0100
    }
}