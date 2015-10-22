namespace CompanySampleDataImporter.Importer.Importers
{
    using System;
    using System.IO;
    using System.Linq;
    using Contracts;
    using Data;

    public class ReportsImporter:IImporter
    {
        private const int NumberOfReports = 250000;

        public string Message
        {
            get { return "Importing reports"; }
        }

        public int Order
        {
            get { return 5; }
        }

        public Action<CompanyEntities, TextWriter> Get
        {
            get 
            { 
                 return (db, tr) =>
                {
                    var employeesIds = db.Employees
                                       .Select(e => e.Id)
                                       .ToList();

                    for (int i = 0; i < employeesIds.Count; i++)
                    {
                        var numberOfReports = RandomGenerator.GetRandomNumber(25, 75);

                        for (int j = 0; j < numberOfReports; j++)
                        {
                            var report = new Report
                            {
                                EmployeeId = employeesIds[i],
                                Time = RandomGenerator.GetRandomDate(before: DateTime.Now)
                            };

                            db.Reports.Add(report);
                        }

                        tr.Write(".");
                        db.SaveChanges();
                        db.Dispose();
                        db = new CompanyEntities();
                    }
                };
            }
        }
    }
}
