namespace DAOClass.Tests
{
    using System;
    using System.Linq;
    using CreatingDAOClass;
    using CreatingDbContextForNorthwind;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DAOClassTests
    {
        [TestMethod]
        public void AddCustomer_ShouldAddANewCustomer()
        {
            using (var db = new NorthwindEntities())
            {
                string city = "City of angels";

                var customersCountBeforeAdding = db.Customers
                                                          .Select(c => c.City == city)
                                                          .ToList()
                                                          .Count;

                Customer newCustomer = new Customer()
                {
                    CustomerID = "LBCDM",
                    CompanyName = "Telerik",
                    ContactName = "Alex Frost",
                    ContactTitle = "Very special guy",
                    Address = "31 Al.Malinov",
                    City = city,
                    Region = null,
                    PostalCode = "2000",
                    Country = "Bulgaria",
                    Phone = "55555555",
                    Fax = null
                };

                DAO.InsertCustomer(newCustomer);

                var customersCountAfterAddingOne = db.Customers
                                                       .Select(c => c.City == city)
                                                       .ToList()
                                                       .Count;

                Assert.AreEqual(customersCountBeforeAdding + 1, customersCountAfterAddingOne);

                DAO.DeleteCustomer(newCustomer);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddCustomer_ShouldThrowAnExceptionWhenTryingToAddCustomersWithTheSameId()
        {
            using (var db = new NorthwindEntities())
            {
                Customer newCustomer = new Customer()
                {
                    CustomerID = "LBCDM",
                    CompanyName = "Telerik",
                    ContactName = "Alex Frost",
                    ContactTitle = "Very special guy",
                    Address = "31 Al.Malinov",
                    City = "City of angels",
                    Region = null,
                    PostalCode = "2000",
                    Country = "Bulgaria",
                    Phone = "55555555",
                    Fax = null
                };

                DAO.InsertCustomer(newCustomer);
                DAO.InsertCustomer(newCustomer);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void DeleteCustomer_ShouldThrowAnExceptionWhenTryingToDeleteNonExistentCustomer()
        {
            using (var db = new NorthwindEntities())
            {
                Customer newCustomer = new Customer()
                {
                    CustomerID = "XXXXX",
                    CompanyName = "Telerik",
                    ContactName = "Alex Frost",
                    ContactTitle = "Very special guy",
                    Address = "31 Al.Malinov",
                    City = "City of angels",
                    Region = null,
                    PostalCode = "2000",
                    Country = "Bulgaria",
                    Phone = "55555555",
                    Fax = null
                };

                DAO.DeleteCustomer(newCustomer);
            }
        }

        [TestMethod]
        public void DeleteCustomer_ShouldDeleteACustomer()
        {
            using (var db = new NorthwindEntities())
            {
                string city = "City of angels";

                Customer newCustomer = new Customer()
                {
                    CustomerID = "LBCDM",
                    CompanyName = "Telerik",
                    ContactName = "Alex Frost",
                    ContactTitle = "Very special guy",
                    Address = "31 Al.Malinov",
                    City = city,
                    Region = null,
                    PostalCode = "2000",
                    Country = "Bulgaria",
                    Phone = "55555555",
                    Fax = null
                };

                var customersCountBeforeDeleting = db.Customers
                                                          .Select(c => c.City == city)
                                                          .ToList()
                                                          .Count;
                DAO.DeleteCustomer(newCustomer);

                var customersCountAfterDeleting = db.Customers
                                                          .Select(c => c.City == city)
                                                          .ToList()
                                                          .Count;

                Assert.AreEqual(customersCountBeforeDeleting - 1, customersCountAfterDeleting);
            }
        }

        [TestMethod]
        public void DeleteCustomer_ShouldDeleteTheCustomerWithTheGivenId()
        {
            using (var db = new NorthwindEntities())
            {
                string id = "LBCDM";
                string city = "City of angels";

                Customer newCustomer = new Customer()
                {
                    CustomerID = id,
                    CompanyName = "Telerik",
                    ContactName = "Alex Frost",
                    ContactTitle = "Very special guy",
                    Address = "31 Al.Malinov",
                    City = city,
                    Region = null,
                    PostalCode = "2000",
                    Country = "Bulgaria",
                    Phone = "55555555",
                    Fax = null
                };

                DAO.InsertCustomer(newCustomer);
                DAO.DeleteCustomer(newCustomer);

                var customerAfterDeleting = db.Customers
                                              .FirstOrDefault(c => c.CustomerID == id);

                Assert.AreEqual(null, customerAfterDeleting);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ModifyCustomer_ShouldThrowAnExceptionWhenTryingToModifyNonExistentCustomer()
        {
            using (var db = new NorthwindEntities())
            {
                Customer newCustomer = new Customer()
                {
                    CustomerID = "XXXXX",
                    CompanyName = "Telerik",
                    ContactName = "Alex Frost",
                    ContactTitle = "Very special guy",
                    Address = "31 Al.Malinov",
                    City = "City of angels",
                    Region = null,
                    PostalCode = "2000",
                    Country = "Bulgaria",
                    Phone = "55555555",
                    Fax = null
                };

                DAO.ModifyCustomer(newCustomer);
            }
        }

        [TestMethod]
        public void ModifyCustomer_ShouldModifyCustomersComapnyNameAndCompanyTitle()
        {
            using (var db = new NorthwindEntities())
            {
                string id = "LBCDM";
                string city = "City of angels";

                Customer newCustomer = new Customer()
                {
                    CustomerID = id,
                    CompanyName = "Telerik",
                    ContactName = "Alex Frost",
                    ContactTitle = "Very special guy",
                    Address = "31 Al.Malinov",
                    City = city,
                    Region = null,
                    PostalCode = "2000",
                    Country = "Bulgaria",
                    Phone = "55555555",
                    Fax = null
                };

                DAO.InsertCustomer(newCustomer);

                DAO.ModifyCustomer(newCustomer);

                var customer = db.Customers
                                .FirstOrDefault(c => c.CustomerID == id);

                Assert.AreNotEqual(newCustomer.ContactTitle, customer.ContactTitle);
                Assert.AreNotEqual(newCustomer.CompanyName, customer.CompanyName);

                DAO.DeleteCustomer(newCustomer);
            }
        }
    }
}
