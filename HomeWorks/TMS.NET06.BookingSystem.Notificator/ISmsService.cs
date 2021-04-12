namespace TMS.NET06.BookingSystem.Notificator
{
    internal interface ISmsService
    {
        void SendSms(string phoneNumber, string text);
    }
}