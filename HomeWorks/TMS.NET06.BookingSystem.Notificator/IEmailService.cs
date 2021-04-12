namespace TMS.NET06.BookingSystem.Notificator
{
    internal interface IEmailService
    {
        void SendEmail(string emailAddress, string subject, string text);
    }
}