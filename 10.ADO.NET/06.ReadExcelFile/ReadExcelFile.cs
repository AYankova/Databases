namespace _06.ReadExcelFile
{
    using System;
    using System.Data.OleDb;

    public class ReadExcelFile
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

                OleDbCommand command = new OleDbCommand("SELECT * FROM [" + sheetName + "]", dbConnection);

                OleDbDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        var name = reader["Name"];
                        var score = reader["Score"];

                        Console.WriteLine("Name: {0, 15} -- > Scores: {1, 2}", name, score);
                    }
                }
            }
        }
    }
}
