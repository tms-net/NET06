namespace TMS.NET06.BookingSystem.Notificator
{
    public interface IEmailService
    {
        void SendEmail(string emailAddress, string subject, string text);
    }
}