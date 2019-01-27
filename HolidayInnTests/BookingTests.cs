using Edument.CQRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HolidayInn.Booking;
using Events.Booking;
using NUnit.Framework;

namespace HolidayInnTests
{
    [TestFixture]
    public class BookingTests : BDDTest<BookingAggregate>
    {
        private Guid testId;
        private string testName;
        private string testPhoneNumber;
        private string testEmail;
        private DateTime testCheckIn;
        private DateTime testCheckOut;
        private DateTime testCheckOut2030;

        [SetUp]
        public void Setup()
        {
            testId = Guid.NewGuid();
            testName = "Rob";
            testPhoneNumber = "0478566555";
            testEmail = "happyDays@gmail.com";
            testCheckIn = new DateTime(2019, 01, 01);
            testCheckOut = new DateTime(2019, 01, 20);
            testCheckOut2030 = new DateTime(2030, 01, 29);
        }

        /// <summary>
        /// Testing
        /// </summary>
        [Test]
        public void CanCreateBooking()
        {
            Test(
                Given(),
                When(new MakeBooking
                {
                    Id = testId,
                    Name = testName,
                    PhoneNumber = testPhoneNumber,
                    Email = testEmail,
                    CheckInDate = testCheckIn,
                    CheckOutDate = testCheckOut
                }),
                Then(new BookingMade
                {
                    Id = testId,
                    Name = testName,
                    PhoneNumber = testPhoneNumber,
                    Email = testEmail,
                    CheckInDate = testCheckIn,
                    CheckOutDate = testCheckOut
                }));
        }

        [Test]
        public void CanNotCancelWithoutBooking()
        {
            Test(
                Given(),
                When(new CancelBooking
                {
                    Id = testId

                }),
                ThenFailWith<NoBookingMade>());

        }

        [Test]
        public void CanCancelBooking()
        {

            Test(
                Given(new BookingMade
                {
                    Id = testId,
                    Name = testName,
                    PhoneNumber = testPhoneNumber,
                    Email = testEmail,
                    CheckInDate = testCheckIn,
                    CheckOutDate = testCheckOut2030
                }),
                When(new CancelBooking
                {
                    Id = testId

                }),
                Then(new BookingCancelled
                {
                    Id = testId
                }));
               

        }

        [Test]
        public void CanNotCancelBookingAsCheckOutDateHasPassed()
        {
            Test(
                Given(new BookingMade
                {
                    Id = testId,
                    Name = testName,
                    PhoneNumber = testPhoneNumber,
                    Email = testEmail,
                    CheckInDate = testCheckIn,
                    CheckOutDate = testCheckOut
                }),
                When(new CancelBooking
                {
                    Id = testId

                }),
                ThenFailWith<AlreadyPassedCheckoutDate>());
        }

        public void CanNotCancelAnAlreadyCancelledBooking()
        {
            Test(
                Given(new BookingMade
                {
                    Id = testId,
                    Name = testName,
                    PhoneNumber = testPhoneNumber,
                    Email = testEmail,
                    CheckInDate = testCheckIn,
                    CheckOutDate = testCheckOut2030
                },
                new BookingCancelled
                {
                    Id = testId
                }),
                When(new CancelBooking
                {
                    Id = testId

                }),
                ThenFailWith<BookingAlreadyCancelled>());

        }
    }
}
