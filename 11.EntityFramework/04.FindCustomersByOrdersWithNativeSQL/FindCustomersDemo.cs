namespace FindCustomersByOrdersWithNativeSQL
{
    using System;
    using CreatingDbContextForNorthwind;

    public class FindCustomersDemo
    {
        public static void Main()
        {
            var query = "SELECT * FROM Customers c, Orders o " +
                        "WHERE c.CustomerID = o.CustomerID " +
                        "AND YEAR(o.OrderDate) = 1997 " +
                        "AND o.ShipCountry = 'Canada'";

            using (var db = new NorthwindEntities())
            {
                var customers = db.Database.SqlQuery<Customer>(query);

                Console.WriteLine("Customers' contact names: ");

                foreach (var customer in customers)
                {
                    Console.WriteLine(customer.ContactName);
                }
            }
        }
    }
}
