namespace TMS.NET06.BookingSystem.Notificator
{
    public interface IEmailService
    {
        bool SendEmail(string emailAddress, string subject, string text);
    }
}