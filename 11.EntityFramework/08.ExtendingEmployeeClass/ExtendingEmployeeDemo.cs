namespace ExtendingEmployeeClass
{
    using System;
    using System.Linq;
    using CreatingDbContextForNorthwind;

    public class ExtendingEmployeeDemo
    {
        public static void Main()
        {
            // The extension class is in the first console app in EmployeeExtension.cs
            using (var db = new NorthwindEntities())
            {
                foreach (var employee in db.Employees)
                {
                    var territoriesIds = employee.Territories.Select(t => t.TerritoryID);
                    Console.WriteLine("Employee name: {0}, Territories IDs: {1}", employee.FirstName, string.Join(", ", territoriesIds));
                }
            }
        }
    }
}