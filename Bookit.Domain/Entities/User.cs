using System;
using System.Collections.Generic;

namespace Bookit.Domain
{
    /// <summary>
    /// User makes the reservation and receives the invoice
    /// </summary>
    [Serializable]
    public class User
    {
        public string Name { get; set; }

        /// <summary>
        /// List of bookings made by this user
        /// </summary>
        public List<Booking> Reservations  = new List<Booking>();
    }
}
