using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayInn.Booking
{
    public class NoBookingMade : Exception { }

    public class AlreadyPassedCheckoutDate : Exception { }

    public class BookingAlreadyCancelled : Exception { }
}
