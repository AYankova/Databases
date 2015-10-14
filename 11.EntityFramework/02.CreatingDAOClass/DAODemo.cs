namespace CreatingDAOClass
{
    using System;
    using System.Linq;
    using CreatingDbContextForNorthwind;

    public class DAODemo
    {
        public static void Main()
        {
            var customer = new Customer
            {
                CustomerID = "PHPTH",
                CompanyName = "Telerik Academy",
                ContactName = "Pesho Petrov",
                ContactTitle = "Marketing Assistant",
                Address = "31 Al. Malinov",
                City = "Sofia",
                Region = null,
                PostalCode = "2100",
                Country = "Bulgaria",
                Phone = "(359) 2444444",
                Fax = null
            };

            try
            {
                Console.WriteLine("INSERT:");
                int affectedRowsAfterInsert = DAO.InsertCustomer(customer);
                Console.WriteLine("{0} row(s) affected", affectedRowsAfterInsert);
                Console.WriteLine("MODIFY:");
                int affectedRowsAfterModify = DAO.ModifyCustomer(customer);
                Console.WriteLine("{0} row(s) affected", affectedRowsAfterModify);
                Console.WriteLine("DELETE");
                int affectedRowsAfterDelete = DAO.DeleteCustomer(customer);
                Console.WriteLine("{0} row(s) affected", affectedRowsAfterDelete);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
