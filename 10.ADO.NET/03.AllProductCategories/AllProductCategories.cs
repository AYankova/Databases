namespace _03.AllProductCategories
{
    using System;
    using System.Data.SqlClient;

    public class AllProductCategories
    {
        public static void Main()
        {
            SqlConnection dbConnection = new SqlConnection("Server=.; " +
            "Database=Northwind; Integrated Security=true");

            dbConnection.Open();

            using (dbConnection)
            {
                SqlCommand command = new SqlCommand(
                    "SELECT c.CategoryName, p.ProductName " +
                    "FROM Categories c " +
                    "JOIN Products p " +
                    "ON c.CategoryID = p.CategoryID " +
                    "ORDER BY c.CategoryName",
                    dbConnection);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string categoryName = (string)reader["categoryName"];
                        string productName = (string)reader["productName"];
                        Console.WriteLine("{0} --> {1}", categoryName, productName);
                    }
                }
            }
        }
    }
}
