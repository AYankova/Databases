namespace _09.MySQLBooks
{
    using System;
    using System.Configuration;
    using MySql.Data.MySqlClient;

    public class MySQLBooksDB
    {
        public static void Main()
        {
            string connectionStringForMySql = ConfigurationManager.ConnectionStrings["MySQLBooks"].ConnectionString;

            ListAllBooks(connectionStringForMySql);

            AddBookToDB("Thorn Birds", "Colleen McCullough", new DateTime(1989, 09, 20), "0060837551000", connectionStringForMySql);

            FindBookByName(connectionStringForMySql);
        }

        private static void ListAllBooks(string connectionString)
        {
            MySqlConnection dbConnection = new MySqlConnection(connectionString);

            dbConnection.Open();
            Console.WriteLine("***Listing all books***");

            using (dbConnection)
            {
                MySqlCommand command = new MySqlCommand(
                                                        @"SELECT Title, Author, PublishDate, ISBN
                                                        FROM Books",
                                                        dbConnection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var title = reader["Title"].ToString();
                        var author = reader["Author"].ToString();
                        var publishDate = DateTime.Parse(reader["PublishDate"].ToString());
                        var isbn = reader["ISBN"].ToString();

                        var book = new Book
                        {
                            Title = title,
                            Author = author,
                            PublishDate = publishDate,
                            Isbn = isbn
                        };

                        Console.WriteLine(book);
                    }
                }
            }
        }

        private static void AddBookToDB(string title, string author, DateTime? publishDate, string isbn, string connectionString)
        {
            MySqlConnection dbConnection = new MySqlConnection(connectionString);

            dbConnection.Open();
            Console.WriteLine("***Adding books***");

            using (dbConnection)
            {
                var command = new MySqlCommand(
                                                @"INSERT INTO Books (Title, Author, PublishDate, ISBN)
                                                VALUES (@title, @author, @publishDate, @isbn)",
                                                dbConnection);

                command.Parameters.AddWithValue("@title", title);
                command.Parameters.AddWithValue("@author", author);
                command.Parameters.AddWithValue("@publishDate", publishDate);
                command.Parameters.AddWithValue("@isbn", isbn);

                var queryResult = command.ExecuteNonQuery();
                command.Parameters.Clear();

                Console.WriteLine("({0} row(s) affected)", queryResult);

                MySqlCommand cmdSelectIdentity = new MySqlCommand("SELECT @@Identity", dbConnection);
                var insertedRecordId = cmdSelectIdentity.ExecuteScalar();
                Console.WriteLine("Book was inserted succesfully. BookID = {0}", insertedRecordId);
            }
        }

        private static void FindBookByName(string connectionString)
        {
            MySqlConnection dbConnection = new MySqlConnection(connectionString);

            dbConnection.Open();
            Console.WriteLine();
            Console.WriteLine("***Enter a name of a book to search***");
            string input = Console.ReadLine();

            using (dbConnection)
            {
                MySqlCommand command = new MySqlCommand(string.Format(@"SELECT Title, Author, PublishDate, ISBN FROM books  
                                                                      WHERE Title LIKE '%{0}%'",
                                                                      input),
                                                        dbConnection);

                MySqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        var title = reader["Title"].ToString();
                        var author = reader["Author"].ToString();
                        var publishDate = DateTime.Parse(reader["PublishDate"].ToString());
                        var isbn = reader["ISBN"].ToString();

                        var book = new Book
                        {
                            Title = title,
                            Author = author,
                            PublishDate = publishDate,
                            Isbn = isbn
                        };

                        Console.WriteLine(book);
                    }
                }
            }
        }
    }
}
