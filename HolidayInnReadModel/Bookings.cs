using Edument.CQRS;
using Events.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayInnReadModel
{
    public class Bookings : IBookingQueries,
        ISubscribeTo<BookingMade>,
        ISubscribeTo<BookingCancelled>
    {
        public class BookingItem
        {
            public Guid BookingId;
            public string Name;
            public string PhoneNumber;
            public string Email;
            public DateTime CheckIn;
            public DateTime CheckOut;
            public bool HasBeenCancelled;
            public DateTime TimeOfCancelation;
        }

        private List<BookingItem> m_Bookings = new List<BookingItem>();

        public void Handle(BookingMade e)
        {
            lock (m_Bookings)
            {
                var booking = new BookingItem
                {
                    BookingId = e.Id,
                    Name = e.Name,
                    PhoneNumber = e.PhoneNumber,
                    Email = e.Email,
                    CheckIn = e.CheckInDate,
                    CheckOut = e.CheckOutDate
                };

                m_Bookings.Add(booking);
            }
        }

        public void Handle(BookingCancelled e)
        {
            lock (m_Bookings)
            {
                var cancelledBooking = m_Bookings.SingleOrDefault(booking => booking.BookingId == e.Id);
                if (cancelledBooking != null)
                {
                    cancelledBooking.HasBeenCancelled = true;
                    cancelledBooking.TimeOfCancelation = DateTime.Now;
                }
            }
        }

        /// <summary>
        /// Get a list of all bookings
        /// </summary>
        /// <returns></returns>
        public List<BookingItem> AllBookings()
        {
            return m_Bookings;
        }

        /// <summary>
        /// Get a list of all cancelled bookings
        /// </summary>
        /// <returns></returns>
        public List<BookingItem> AllCancelledBookings()
        {
            return m_Bookings.Where(booking => booking.HasBeenCancelled == true).ToList();
        }

        /// <summary>
        /// Get a list of bookings based on an Email Address
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public List<BookingItem> BookingsForEmailAddress(string emailAddress)
        {
            if(string.IsNullOrEmpty(emailAddress))
                return new List<BookingItem>();

            return m_Bookings.Where(booking => booking.Email.ToUpper() == emailAddress.ToUpper()).ToList();
        }
    }
}
