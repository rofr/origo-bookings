using System;
using System.Collections.Generic;
using OrigoDB.Core;

namespace Bookit.Domain
{

    /// <summary>
    /// An instance of this class is the single in-memory aggregate
    /// </summary>
    [Serializable]
    public class BookingsModel : Model
    {
        /// <summary>
        /// The bookable resources keyed by id
        /// </summary>
        public readonly Dictionary<int, Resource> Resources 
            = new Dictionary<int, Resource>();


        public readonly Dictionary<int, Booking> BookingsById 
            = new Dictionary<int, Booking>(); 

        /// <summary>
        /// Booking index keyed by the first booking day, each value is a 
        /// list of bookings starting that particular day
        /// </summary>
        public readonly Dictionary<DateTime, List<Booking>> BookingsByStartDate = 
            new Dictionary<DateTime, List<Booking>>();


        public readonly Dictionary<string,User> Users =
            new Dictionary<string, User>();

        /// <summary>
        /// Invoice at position p has id p + 1
        /// </summary>
        public readonly List<Invoice> Invoices
            = new List<Invoice>();

        

        public void AddInvoice(Invoice invoice)
        {
            invoice.SetId(Invoices.Count + 1);
            Invoices.Add(invoice);
        }


        private int _nextBookingId = 1;

        public int AddBooking(Booking booking)
        {
            booking.SetBookingId(_nextBookingId++);
            var begins = booking.Period.Begins;

            // add to the index
            if (!BookingsByStartDate.ContainsKey(begins))
            {
                BookingsByStartDate[begins] = new List<Booking>();
            }
            BookingsByStartDate[begins].Add(booking);

            BookingsById.Add(booking.BookingId, booking);

            return booking.BookingId;
        }
    }
}
