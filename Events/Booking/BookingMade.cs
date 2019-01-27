using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Booking
{
    public class BookingMade
    {
        public Guid Id;
        public string Name;
        public string PhoneNumber;
        public string Email;
        public DateTime CheckInDate;
        public DateTime CheckOutDate;
    }
}
