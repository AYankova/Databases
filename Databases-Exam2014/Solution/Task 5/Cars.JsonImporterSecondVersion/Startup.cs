namespace Cars.JsonImporterSecondVersion
{
    using System.Data.Entity;
    using Cars.Data;
    using Cars.Data.Migrations;

    public class Startup
    {
        public static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CarsDbContext, Configuration>());

            // JsonImporter.Import();
            XmlCarsSearcher.Search();
        }
    }
}
