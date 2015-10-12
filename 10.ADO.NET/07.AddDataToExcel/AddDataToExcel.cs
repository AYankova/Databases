namespace _07.AddDataToExcel
{
    using System;
    using System.Data.OleDb;

    public class AddDataToExcel
    {
        public static void Main()
        {
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=..\..\ExcelDocs\trainers.xlsx;Extended Properties='Excel 12.0 Xml;HDR=YES/'";

            OleDbConnection dbConnection = new OleDbConnection(connectionString);

            dbConnection.Open();

            using (dbConnection)
            {
                var excelSchema = dbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                var sheetName = excelSchema.Rows[0]["TABLE_NAME"].ToString();

                OleDbCommand command = new OleDbCommand("INSERT INTO [" + sheetName + "] VALUES (@name, @scores)", dbConnection);

                command.Parameters.AddWithValue("@name", "Pesho Goshov");
                command.Parameters.AddWithValue("@scores", "11");

                try
                {
                    for (var i = 0; i < 5; i++)
                    {
                        var queryResult = command.ExecuteNonQuery();
                        Console.WriteLine("({0} row(s) affected)", queryResult);
                    }
                }
                catch (OleDbException exception)
                {
                    Console.WriteLine("SQL Error occured: " + exception);
                }
            }
        }
    }
}
