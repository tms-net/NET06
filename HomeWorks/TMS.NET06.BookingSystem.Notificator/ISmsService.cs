namespace TMS.NET06.BookingSystem.Notificator
{
    public interface ISmsService
    {
        void SendSms(string phoneNumber, string text);
    }
}