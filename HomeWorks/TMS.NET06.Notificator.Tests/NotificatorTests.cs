using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
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
            //arrange
            var bookingEntries = new[] { new BookEntry
                {
                    Comment = "I forget you Bill",
                    BookId = 5,
                    Client = new Client
                    {
                        Name = "Hichkok",
                        ContactInformation = new ContactInfo
                        {
                            Email = "pupkin@tut.by",
                            PhoneNumber = "+375293223322"
                        },
                        ClientId = 25,
                    },
                    Service = new Service
                    {
                        Cost = 2,
                        Duration = TimeSpan.FromMinutes(10),
                        Name = "Утиная холка",
                        ServiceId = 15
                    },
                    Status = BookingStatus.Confirmed,
                    VisitDate = DateTime.UtcNow
                }
            };
            var bookingRepo = new Mock<IBookingRepository>();
            bookingRepo
                .Setup(repo => repo.GetBookingEntriesAsync(
                    It.IsAny<DateTime>(), It.IsAny<DateTime>(), BookingStatus.Confirmed))
                .ReturnsAsync(bookingEntries);

            var emailService = new Mock<IEmailService>();
            var smsService = new Mock<ISmsService>();
            var notificator = new NotificationService(
                bookingRepo.Object,
                emailService.Object,
                smsService.Object);

            //act
            notificator.SendNotifications(TimeSpan.Zero);

            //assert
            Assert.IsNotNull(bookingEntries[0].NotificationInfo);
            Assert.IsNotNull(bookingEntries[0].NotificationInfo.EmailSentDate);

            //emailService.Verify(w => w.SendEmail());

            //bookingRepo.Verify(
            //    repo => repo.SaveEntryAsync(It.IsAny<BookEntry>()),
            //    Times.AtLeastOnce);
        }

        [Test]
        public void SendNotificationsShouldSaveUpdatedEntry()
        {
            // arrange
            var bookingRepo = new Mock<IBookingRepository>();
            bookingRepo
                .Setup(repo => repo.GetBookingEntriesAsync(
                    It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<BookingStatus?>()))
                .ReturnsAsync(new[] {new BookEntry()});

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
                repo => repo.SaveEntryAsync(It.IsAny<BookEntry>()),
                Times.AtLeastOnce);
        }
    }
}