using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.NET06.BookingSystem.Notificator
{
    public class NotificationService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IEmailService _emailService;
        private readonly ISmsService _smsService;

        public NotificationService(
                IBookingRepository bookingRepository,
                IEmailService emailService,
                ISmsService smsService)
        {
            _bookingRepository = bookingRepository;
            _emailService = emailService;
            _smsService = smsService;
        }

        public async void SendNotifications(TimeSpan period)
        {
            var now = DateTime.UtcNow;
            var entries = await _bookingRepository
                .GetBookingEntriesAsync(now, now + period, BookingStatus.Confirmed);

            foreach (BookEntry entry in entries)
            {
                if (!string.IsNullOrEmpty(entry.Client?.ContactInformation?.Email) &&
                    entry.NotificationInfo?.EmailSentDate == null)
                {
                    var text = $"You have appointment for {entry.Service.Name} on {entry.VisitDate:g}";
                    try
                    {
                        _emailService.SendEmail(
                            entry.Client?.ContactInformation.Email,
                            "Katcherlash appointment",
                            text);
                        entry.NotificationInfo.EmailSentDate = DateTime.UtcNow;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                if (!string.IsNullOrEmpty(entry.Client?.ContactInformation?.PhoneNumber) &&
                    entry.NotificationInfo?.EmailSentDate == null)
                {
                    try
                    {
                        var text = $"You have appointment for {entry.Service.Name} on {entry.VisitDate:g}";
                        _smsService.SendSms(
                            entry.Client.ContactInformation.PhoneNumber, text);
                        entry.NotificationInfo.SmsSentDate = DateTime.UtcNow;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

                await _bookingRepository.SaveEntryAsync(entry);
            }
        }
    }
}
