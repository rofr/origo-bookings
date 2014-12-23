using System;
using System.IO;
using Bookit.Domain;
using OrigoDB.Core;

namespace Bookit.Demo
{
    class Program
    {
        private static Engine<BookingsModel> _engine;

        static void Main(string[] args)
        {
            if (Directory.Exists("BookingsModel")) Directory.Delete("BookingsModel", true);
            Directory.CreateDirectory("BookingsModel");


            var initialModel = CreateInitialModel();
            
            _engine = Engine.Create<BookingsModel>(initialModel);
            AddSuccesfulBooking();
            TryAddConflictingBooking();
            var invoiceCommand = new CreateInvoicesCommand(DateTime.Today.AddDays(2));
            var numInvoicesGenerated = _engine.Execute(invoiceCommand);
            Console.WriteLine("Generated {0} invoices, should be 1", numInvoicesGenerated);

            //do it again, should be nothing to invoice
            numInvoicesGenerated = _engine.Execute(invoiceCommand);
            Console.WriteLine("Generated {0} invoices, should be 0", numInvoicesGenerated);
            

            Console.ReadLine();
        }

        private static void TryAddConflictingBooking()
        {

            var command2 = new AddBookingCommand()
            {
                UserId = "Bart",
                Period = new DateRange(DateTime.Today.AddDays(2), DateTime.Today.AddDays(6)),
                ResourceId = 1
            };
            try
            {
                _engine.Execute(command2);
            }
            catch (CommandAbortedException ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        private static void AddSuccesfulBooking()
        {

            var command1 = new AddBookingCommand()
            {
                UserId = "Homer",
                Period = new DateRange(DateTime.Today, DateTime.Today.AddDays(4)),
                ResourceId = 1
            };
           _engine.Execute(command1);
        }

        private static BookingsModel CreateInitialModel()
        {
            var initialModel = new BookingsModel();

            var user1 = new User() {Name = "Bart"};
            var user2 = new User {Name = "Homer"};

            initialModel.Users.Add(user1.Name, user1);
            initialModel.Users.Add(user2.Name, user1);

            var resource1 = new House(1) {Name = "Bungalow small", PricePerDay = 800};
            var resource2 = new House(2) {Name = "Bungalow big", PricePerDay = 1200};

            initialModel.Resources.Add(1, resource1);
            initialModel.Resources.Add(2, resource2);
            return initialModel;
        }
    }
}
