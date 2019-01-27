using Edument.CQRS;
using Events.Booking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayInn.Booking
{
    public class BookingAggregate : Aggregate,
        IHandleCommand<MakeBooking>,
        IHandleCommand<CancelBooking>,
        IApplyEvent<BookingMade>,
        IApplyEvent<BookingCancelled>

    {
        //Date of Checkout
        private DateTime m_CheckOutDate;
        //Booking Made
        private bool m_BookingMade;
        //Has the booking been cancelled
        private bool m_BookingCancelled;
        //Date when booking was cancelled
        private DateTime m_DateOfCancelation;

        public IEnumerable Handle(MakeBooking c)
        {
            yield return new BookingMade
            {
                Id = c.Id,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                CheckInDate = c.CheckInDate,
                CheckOutDate = c.CheckOutDate
            };
        }

        public void Apply(BookingMade e)
        {
            //Keep track of the checkout date
            m_CheckOutDate = e.CheckOutDate;
            m_BookingMade = true;
        }

        public IEnumerable Handle(CancelBooking c)
        {
            if (!m_BookingMade)
                throw new NoBookingMade();

            if (DateTime.Now.Ticks > m_CheckOutDate.Ticks)
                throw new AlreadyPassedCheckoutDate();

            if (m_BookingCancelled)
                throw new BookingAlreadyCancelled();

            yield return new BookingCancelled
            {
                Id = c.Id
            };
        }

        public void Apply(BookingCancelled e)
        {
            m_DateOfCancelation = DateTime.Now;
            m_BookingCancelled = true;
        }
    }
}
