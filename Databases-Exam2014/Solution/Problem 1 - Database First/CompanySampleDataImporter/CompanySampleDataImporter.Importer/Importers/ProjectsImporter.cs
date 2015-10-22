namespace CompanySampleDataImporter.Importer.Importers
{
    using System;
    using System.IO;
    using System.Linq;
    using CompanySampleDataImporter.Data;
    using Contracts;

    public class ProjectsImporter : IImporter
    {
        private const int NumberOfProjects = 1000;

        public string Message
        {
            get { return "Importing projects"; }
        }

        public int Order
        {
            get { return 4; }
        }

        public Action<CompanyEntities, TextWriter> Get
        {
            get
            {
                return (db, tr) =>
                {
                    var allEmployeesIds = db.Employees
                                          .OrderBy(e => Guid.NewGuid())
                                          .Select(e => e.Id)
                                          .ToList();

                    var currentEmployeeIndex = 0;
                    for (int i = 0; i < NumberOfProjects; i++)
                    {
                        var currentProject = new Project
                            {
                                Name = RandomGenerator.GetRandomString(5, 50)
                            };

                        var numberOfEmployeesPerProject = RandomGenerator.GetRandomNumber(2, 8);

                        for (int j = 0; j < numberOfEmployeesPerProject; j++)
                        {
                            if (j + currentEmployeeIndex >= allEmployeesIds.Count)
                            {
                                break;
                            }

                            var currentEmployeeId = allEmployeesIds[currentEmployeeIndex];
                            var startDate = RandomGenerator.GetRandomDate(before: DateTime.Now.AddDays(-100));
                            currentProject.ProjectsEmployees.Add(new ProjectsEmployee
                                {
                                    EmployeeId = currentEmployeeId,
                                    StartDate = startDate,
                                    EndDate = RandomGenerator.GetRandomDate(after: startDate)
                                });
                            currentEmployeeIndex++;
                        }

                        db.Projects.Add(currentProject);

                        if (i % 10 == 0)
                        {
                            tr.Write(".");
                        }

                        if (i % 100 == 0)
                        {
                            db.SaveChanges();
                            db.Dispose();
                            db = new CompanyEntities();
                        }
                    }

                    db.SaveChanges();
                };
            }
        }
    }
}
