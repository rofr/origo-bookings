using System;
using System.Collections.Generic;
using System.Linq;
using OrigoDB.Core;

namespace Bookit.Domain
{
    [Serializable]
    public class GetResourcesAvailableQuery : Query<BookingsModel, List<ResourceView>>
    {
        /// <summary>
        /// Find resources available during this period
        /// </summary>
        public readonly DateRange Range;

        public GetResourcesAvailableQuery(DateRange range)
        {
            Range = range;
        }

        public override List<ResourceView> Execute(BookingsModel model)
        {

            return model
                .Resources
                .Values
                .Where(r => !r.Bookings.Any(b => Range.Overlaps(b.Period)))
                .Select(r => new ResourceView(r)).ToList();
        }
    }
}