namespace CompanySampleDataImporter.Importer
{
    using System;

    public class Startup
    {
        public static void Main()
        {
            SampleDataImporter.Create(Console.Out).Import();
        }
    }
}
