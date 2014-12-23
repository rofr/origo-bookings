using System;

namespace Bookit.Domain
{
    /// <summary>
    /// Represents a reservation
    /// </summary>
    [Serializable]
    public class Booking
    {
        public int BookingId { get; internal set; }

        /// <summary>
        /// The resource being booked.
        /// </summary>
        public readonly Resource Resource;

        /// <summary>
        /// The 
        /// </summary>
        public readonly DateRange Period;

        /// <summary>
        /// The agreed upon price of this reservation
        /// </summary>
        public readonly decimal Price;

        /// <summary>
        /// The user making the booking
        /// </summary>
        public readonly User User;

        public Booking(Resource resource, DateRange period, decimal price, User user)
        {
            Period = period;
            Resource = resource;
            Price = price;
            User = user;
        }

        /// <summary>
        /// The number of the invoice, null means not yet invoiced
        /// </summary>
        public int? InvoiceId { get; private set; }

        public void SetInvoiceId(int id)
        {
            if (InvoiceId.HasValue) throw new InvalidOperationException();
            InvoiceId = id;
        }

        internal void SetBookingId(int id)
        {
            BookingId = id;
        }


        public bool IsInvoiced
        {
            get { return InvoiceId.HasValue; }
        }
    }
}
