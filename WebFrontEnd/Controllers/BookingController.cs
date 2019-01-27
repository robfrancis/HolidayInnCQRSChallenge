using HolidayInn.Booking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebFrontEnd.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        public ActionResult Bookings()
        {
            return View();
        }

        public ActionResult ConfirmBooking(MakeBooking cmd)
        {
            cmd.Id = Guid.NewGuid();
            Domain.Dispatcher.SendCommand(cmd);
            return View();
        }
    }
}