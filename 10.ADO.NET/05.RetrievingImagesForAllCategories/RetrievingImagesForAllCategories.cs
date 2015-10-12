namespace _05.RetrievingImagesForAllCategories
{
    using System;
    using System.Data.SqlClient;
    using System.Drawing;
    using System.IO;

    public class RetrievingImagesForAllCategories
    {
        private const int OleMetaPictureStartPosition = 78;

        public static void Main()
        {
            SqlConnection dbConnection = new SqlConnection("Server=.; " +
           "Database=Northwind; Integrated Security=true");

            dbConnection.Open();

            using (dbConnection)
            {
                SqlCommand command = new SqlCommand("SELECT Picture FROM Categories", dbConnection);
                RetrieveAndStoreImagesFromDB(command);
                Console.WriteLine("Pictures downloaded successfully.");
            }
        }

        private static void RetrieveAndStoreImagesFromDB(SqlCommand command)
        {
            SqlDataReader reader = command.ExecuteReader();

            using (reader)
            {
                var count = 1;

                while (reader.Read())
                {
                    string path = string.Format(@"..\..\Images\image{0}.jpg", count);
                    var picture = (byte[])reader["Picture"];
                    WritePictureToFile(path, picture);
                    count++;
                }
            }
        }

        private static void WritePictureToFile(string path, byte[] picture)
        {
            var memoryStream = new MemoryStream(picture, OleMetaPictureStartPosition, picture.Length - OleMetaPictureStartPosition);

            using (memoryStream)
            {
                using (Image image = Image.FromStream(memoryStream, true, true))
                {
                    image.Save(path);
                }
            }
        }
    }
}
