using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookit.Domain
{
    [Serializable]
    public class Invoice
    {
        public readonly Booking[] Bookings;
            

        public readonly User User;

        public decimal Total
        {
            get { return Bookings.Sum(b => b.Price); }
        }


        public int Id { get; private set; }

        internal void SetId(int id)
        {
            Id = id;
        }

        public Invoice(User user, IEnumerable<Booking> bookings)
        {
            User = user;
            Bookings = bookings.ToArray();
        }
    }
}