using System;
using System.Collections.Generic;
using System.Linq;
using OrigoDB.Core;

namespace Bookit.Domain
{
    [Serializable]
    public class CreateInvoicesCommand : Command<BookingsModel, int>
    {

        public readonly DateTime StartingBefore;

        public CreateInvoicesCommand(DateTime startingBefore)
        {
            StartingBefore = startingBefore;
        }

        /// <summary>
        /// Create invoices for all non-invoiced bookings starting before a specific date
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override int Execute(BookingsModel model)
        {
            var invoices = model.BookingsByStartDate
                .Where(kvp => kvp.Key < StartingBefore)
                .SelectMany(kvp => kvp.Value)
                .Where(k => !k.IsInvoiced)
                .GroupBy(b => b.User)
                .Select(g => new Invoice(g.Key, g.ToList()))
                .ToArray();

            foreach (var invoice in invoices)
            {
                model.AddInvoice(invoice);
                foreach(var booking in invoice.Bookings) booking.SetInvoiceId(invoice.Id);
            }

            return invoices.Length;
        }

    }
}