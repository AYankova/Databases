namespace _08.SearchProductsByPattern
{
    using System;
    using System.Data.SqlClient;

    public class SearchProductsByPattern
    {
        public static void Main()
        {
            string connectionString = "Server=.;Database=Northwind; Integrated Security=true";

            Console.Write("Enter a search pattern: ");
            string input = Console.ReadLine();

            FindMatchingProducts(input, connectionString);
        }

        private static void FindMatchingProducts(string input, string connectionString)
        {
            input = EscapeSymbols(input);
            SqlConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            using (dbConnection)
            {
                SqlCommand command = new SqlCommand(
                                                    @"SELECT ProductName FROM Products
                                                    WHERE ProductName LIKE '%'+ @input +'%'",
                                                    dbConnection);

                command.Parameters.AddWithValue("@input", input);
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["ProductName"]);
                    }
                }
            }
        }

        private static string EscapeSymbols(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '%')
                {
                    input = input.Substring(0, i) + "/" + input.Substring(i, input.Length - i);
                    i++;
                }
            }

            return input;
        }
    }
}
