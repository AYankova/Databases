namespace FindCustomersByOrders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CreatingDbContextForNorthwind;

    public class FindCustomersDemo
    {
        public static void Main()
        {
            var customers = FindCustomersByOrdersMadeIn1997AndShippedToCanada();

            Console.WriteLine("Total count of customers made orders in 1997 and shipped to Canada: {0}", customers.Count);
            Console.WriteLine("Customers' contact names: ");

            foreach (var customer in customers)
            {
                Console.WriteLine(customer.ContactName);
            }

            Console.WriteLine("\nDistinct customers:");
            customers.Distinct().ToList().ForEach(c => Console.WriteLine(c.ContactName));
        }

        private static ICollection<Customer> FindCustomersByOrdersMadeIn1997AndShippedToCanada()
        {
            using (var db = new NorthwindEntities())
            {
                var customers = db.Orders
                               .Where(o => o.OrderDate.Value.Year == 1997 && o.ShipCountry == "Canada")
                               .Select(c => c.Customer)
                               .ToList();

                return customers;
            }
        }
    }
}
