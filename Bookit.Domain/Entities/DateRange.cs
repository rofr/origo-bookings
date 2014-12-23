using System;

namespace Bookit.Domain
{
    [Serializable]
    public struct DateRange
    {
        public readonly DateTime Begins;
        public readonly DateTime Ends;

        public DateRange(DateTime begins, DateTime ends)
        {
            if (begins >= ends) throw new ArgumentOutOfRangeException("ends", "End date must be later then start date");
            Begins = begins;
            Ends = ends;
        }

        public int Days
        {
            get { return (Ends - Begins).Days; }
        }

        public bool Overlaps(DateRange that)
        {
            //todo verify edge cases
            return !(that.Begins > Ends || Begins > that.Ends);
        }
    }
}