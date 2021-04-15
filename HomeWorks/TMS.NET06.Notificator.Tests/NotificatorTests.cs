using Moq;
using NUnit.Framework;
using System;
using TMS.NET06.BookingSystem;
using TMS.NET06.BookingSystem.Notificator;

namespace TMS.NET06.Notificator.Tests
{
    public class NotificatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SendNotificationsShouldProcessConfirmedEntries()
        {
        }

        [Test]
        public void SendNotificationsShouldSendNotificationsForAllAvailableOptions()
        {
        }

        [Test]
        public void SendNotificationsShouldCorectlyUpdateNotificationStatus()
        {
        }

        [Test]
        public void SendNotificationsShouldSaveUpdatedEntry()
        {
            // arrange
            var bookingRepo = new Mock<IBookingRepository>();
            bookingRepo
                .Setup(repo => repo.GetBookingEntries(
                    It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<BookingStatus?>()))
                .Returns(new[] { new BookEntry() });
            var emailService = new Mock<IEmailService>();
            var smsService = new Mock<ISmsService>();
            var notificator = new NotificationService(
                bookingRepo.Object,
                emailService.Object,
                smsService.Object);
            
            // act
            notificator.SendNotifications(TimeSpan.Zero);

            // assert
            bookingRepo.Verify(
                repo => repo.SaveEntry(It.IsAny<BookEntry>()),
                Times.AtLeastOnce);
        }
    }
}