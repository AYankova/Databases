namespace _10.SQLiteBooksDB
{
    using System;
    using System.Data.SQLite;

    public class BooksDB
    {
        public static void Main()
        {
            var connectionStringSQLite = @"Data Source=..\..\DBs\BooksDB.db; Version=3";

            AddBookToDB("Thorn Birds", "Colleen McCullough", new DateTime(1989, 09, 20), "0060837551000", connectionStringSQLite);
            AddBookToDB("Murder on the Orient Express", "Agatha Christie", new DateTime(1983, 10, 15), "9781148758794", connectionStringSQLite);
            AddBookToDB("Dreamcatcher ", "King, Stephen", new DateTime(2001, 05, 14), "9780743211383", connectionStringSQLite);

            ListAllBooks(connectionStringSQLite);
            FindBookByName(connectionStringSQLite);
        }

        private static void AddBookToDB(string title, string author, DateTime? publishDate, string isbn, string connectionString)
        {
            SQLiteConnection dbConnection = new SQLiteConnection(connectionString);

            dbConnection.Open();
            Console.WriteLine("***Adding books***");

            using (dbConnection)
            {
                SQLiteCommand command = new SQLiteCommand(
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
            }
        }

        private static void ListAllBooks(string connectionString)
        {
            SQLiteConnection dbConnection = new SQLiteConnection(connectionString);

            dbConnection.Open();
            Console.WriteLine();
            Console.WriteLine("***Listing all books***");

            using (dbConnection)
            {
                SQLiteCommand command = new SQLiteCommand(
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

        private static void FindBookByName(string connectionString)
        {
            SQLiteConnection dbConnection = new SQLiteConnection(connectionString);

            dbConnection.Open();
            Console.WriteLine();
            Console.WriteLine("***Enter a name of a book to search***");
            string input = Console.ReadLine();

            using (dbConnection)
            {
                SQLiteCommand command = new SQLiteCommand(string.Format(@"SELECT Title, Author, PublishDate, ISBN FROM books  
                                                                      WHERE Title LIKE '%{0}%'",
                                                                      input),
                                                        dbConnection);

                SQLiteDataReader reader = command.ExecuteReader();

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
