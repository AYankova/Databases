namespace _01.CategoriesRowsCount
{
    using System;
    using System.Data.SqlClient;

    public class CategoriesRowsCount
    {
        public static void Main()
        {
            SqlConnection dbConnection = new SqlConnection("Server=.; " +
            "Database=Northwind; Integrated Security=true");

            dbConnection.Open();

            using (dbConnection)
            {
                SqlCommand cmdCount = new SqlCommand(
                    "SELECT COUNT(*) FROM Categories", dbConnection);
                int categoriesCount = (int)cmdCount.ExecuteScalar();

                Console.WriteLine("Categories count: {0}", categoriesCount);
            }
        }
    }
}
