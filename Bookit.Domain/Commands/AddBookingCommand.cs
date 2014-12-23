using System;
using System.Linq;
using OrigoDB.Core;

namespace Bookit.Domain
{
    [Serializable]
    public class AddBookingCommand : Command<BookingsModel, int>
    {
        public int ResourceId;
        public string UserId;
        public DateRange Period;


        //Validate before making any changes to the model

        public override void Prepare(BookingsModel model)
        {
            if (!model.Resources.ContainsKey(ResourceId)) throw new Exception("No such resource");           
            if (!model.Users.ContainsKey(UserId)) throw new Exception("No such user");
            
            //check that the resource is free
            var resource = model.Resources[ResourceId];
            if (resource.Bookings.Any(b => Period.Overlaps(b.Period))) 
                throw new Exception("Resource is not available");
        }

        /// <summary>
        /// Make the reservation and return the booking id. 
        /// Throw if criteria is are not met
        /// </summary>
        public override int Execute(BookingsModel model)
        {
            var resource = model.Resources[ResourceId];
            var user = model.Users[UserId];
            var booking = new Booking(resource, Period, resource.PricePerDay*Period.Days, user);
            resource.Bookings.Add(booking);
            return model.AddBooking(booking);
        }
    }
}
