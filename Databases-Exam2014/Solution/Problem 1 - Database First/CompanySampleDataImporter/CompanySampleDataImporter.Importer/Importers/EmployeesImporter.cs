namespace CompanySampleDataImporter.Importer.Importers
{
    using System;
    using System.IO;
    using System.Linq;
    using Contracts;
    using Data;

    public class EmployeesImporter : IImporter
    {
        private const int NumberOfEmployees = 5000;

        public string Message
        {
            get { return "Importing employees"; }
        }

        public int Order
        {
            get
            {
                return 2;
            }
        }

        public Action<CompanyEntities, TextWriter> Get
        {
            get
            {
                return (db, tr) =>
                {
                    var departmentsIds = db.Departments
                                         .Select(d => d.Id)
                                         .ToList();

                    for (int i = 0; i < NumberOfEmployees; i++)
                    {
                        var randomDepartmentIndex = RandomGenerator.GetRandomNumber(0, departmentsIds.Count() - 1);
                        var randomDepartmentId = departmentsIds[randomDepartmentIndex];

                        db.Employees.Add(new Employee
                        {
                            FirstName = RandomGenerator.GetRandomString(5, 20),
                            LastName = RandomGenerator.GetRandomString(5, 20),
                            YearSalary = RandomGenerator.GetRandomNumber(50000, 200000),
                            DepartmentId = randomDepartmentId
                        });

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
