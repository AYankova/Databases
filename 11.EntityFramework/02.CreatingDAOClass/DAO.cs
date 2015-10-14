namespace CreatingDAOClass
{
    using System;
    using System.Linq;
    using CreatingDbContextForNorthwind;

    public static class DAO
    {
        public static int InsertCustomer(Customer customer)
        {
            using (var db = new NorthwindEntities())
            {
                var customerToAdd = db.Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);

                if (customerToAdd != null)
                {
                    throw new InvalidOperationException(
                        string.Format("Already exists a customer with CustomerID = {0}", customer.CustomerID));
                }

                db.Customers.Attach(customer);
                db.Customers.Add(customer);
                int affectedRows = db.SaveChanges();

                return affectedRows;
            }
        }

        public static int ModifyCustomer(Customer customer)
        {
            using (var db = new NorthwindEntities())
            {
                var customerToModify = db.Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);

                if (customerToModify == null)
                {
                    throw new InvalidOperationException(
                        string.Format("No customer with CustomerID = {0} in the database", customer.CustomerID));
                }

                customerToModify.ContactTitle = db.Customers.FirstOrDefault().ContactTitle;
                customerToModify.CompanyName = db.Customers.FirstOrDefault().CompanyName;
                int affectedRows = db.SaveChanges();

                return affectedRows;
            }
        }

        public static int DeleteCustomer(Customer customer)
        {
            using (var db = new NorthwindEntities())
            {
                var customerToDelete = db.Customers.FirstOrDefault(c => c.CustomerID == customer.CustomerID);

                if (customerToDelete == null)
                {
                    throw new InvalidOperationException(
                        string.Format("No customer with CustomerID = {0} in the database", customer.CustomerID));
                }

                db.Customers.Remove(customerToDelete);
                int affectedRows = db.SaveChanges();

                return affectedRows;
            }
        }
    }
}
