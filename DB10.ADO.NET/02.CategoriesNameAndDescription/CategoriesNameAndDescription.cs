namespace _02.CategoriesNameAndDescription
{
    using System;
    using System.Data.SqlClient;

    public class CategoriesNameAndDescription
    {
        public static void Main()
        {
            SqlConnection dbConnection = new SqlConnection("Server=.; " +
           "Database=Northwind; Integrated Security=true");

            dbConnection.Open();

            using (dbConnection)
            {
                SqlCommand command = new SqlCommand(
                    "SELECT CategoryName, Description FROM Categories", dbConnection);
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        string categoryName = (string)reader["CategoryName"];
                        string description = (string)reader["Description"];
                        Console.WriteLine("{0} --> {1}", categoryName, description);
                    }
                }
            }
        }
    }
}
