using Edument.CQRS;
using HolidayInn.Booking;
using HolidayInnReadModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFrontEnd
{
    public static class Domain
    {
        public static MessageDispatcher Dispatcher;
        public static IBookingQueries BookingQueries;


        public static void StartUp()
        {
            Dispatcher = new MessageDispatcher(new InMemoryEventStore());

            Dispatcher.ScanInstance(new BookingAggregate());

            BookingQueries = new Bookings();
            Dispatcher.ScanInstance(BookingQueries);
        }
    }
}