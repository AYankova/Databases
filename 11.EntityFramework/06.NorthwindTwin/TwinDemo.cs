namespace _NorthwindTwin
{
    using System;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;
    using CreatingDbContextForNorthwind;

    public class TwinDemo
    {
       public static void Main()
        {
            IObjectContextAdapter db = new NorthwindEntities();
            string twin = db.ObjectContext.CreateDatabaseScript();

           // Change the filename
            string createTwinCommandSql = "CREATE DATABASE NorthwindTwin ON PRIMARY " +
            "(NAME = NorthwindTwin, " +
            @"FILENAME = 'D:\\NorthwindTwin.mdf', " +
            "SIZE = 5MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
            "LOG ON (NAME = NorthwindTwinLog, " +
            @"FILENAME = 'D:\\NorthwindTwin.ldf', " +
            "SIZE = 1MB, " +
            "MAXSIZE = 5MB, " +
            "FILEGROWTH = 10%)";

            SqlConnection dbConnectionToCreateDB = new SqlConnection(
                "Server=.; " +
                "Database=master; " +
                "Integrated Security=true");

            dbConnectionToCreateDB.Open();

            using (dbConnectionToCreateDB)
            {
                SqlCommand createDB = new SqlCommand(createTwinCommandSql, dbConnectionToCreateDB);
                createDB.ExecuteNonQuery();
            }

            SqlConnection dbConnectionToClone = new SqlConnection(
                "Server=.; " +
                "Database=NorthwindTwin; " +
                "Integrated Security=true");

            dbConnectionToClone.Open();

            using (dbConnectionToClone)
            {
                SqlCommand cloneDB = new SqlCommand(twin, dbConnectionToClone);
                cloneDB.ExecuteNonQuery();
            }

            Console.WriteLine("Northwind twin created successfully.");
        }
    }
}
