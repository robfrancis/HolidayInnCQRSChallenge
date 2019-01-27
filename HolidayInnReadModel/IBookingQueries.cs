using System.Collections.Generic;

namespace HolidayInnReadModel
{
    public interface IBookingQueries
    {
        List<Bookings.BookingItem> AllBookings();
        List<Bookings.BookingItem> AllCancelledBookings();
        List<Bookings.BookingItem> BookingsForEmailAddress(string Email);
    }
}