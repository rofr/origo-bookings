using System;
using System.Collections.Generic;

namespace Bookit.Domain
{
    [Serializable]
    public abstract class Resource
    {
        public readonly int Id;

        public string Name { get; set; }
        
        public int PricePerDay { get; set; }

        
        protected Resource(int id)
        {
            Id = id;
        }

        /// <summary>
        /// All the past, present and future bookings of this resource
        /// </summary>
        public readonly List<Booking> Bookings
            = new List<Booking>();

    }
}