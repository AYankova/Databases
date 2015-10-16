namespace StudentSystem.ConsoleClient
{
    using System;
    using System.Linq;
    using Data;

    public class Startup
    {
        public static void Main()
        {
            var db = new StudentSystemDbContext();
            db.Database.Initialize(true);
            PrintStudents(db);
            PrintCourses(db);
            PrintHomeworks(db);
        }

        private static void PrintStudents(StudentSystemDbContext db)
        {
            Console.WriteLine("Students: ");
            foreach (var student in db.Students.Include("Courses"))
            {
                Console.WriteLine(" - {0} -> present in {1} course(s)", student.FullName, student.Courses.Count());
            }
        }

        private static void PrintCourses(StudentSystemDbContext db)
        {
            Console.WriteLine("\nCourses: ");
            foreach (var course in db.Courses.Include("Homeworks"))
            {
                Console.WriteLine(" - {0} -> has {1} homework(s)", course.Name, course.Homeworks.Count());
            }
        }

        private static void PrintHomeworks(StudentSystemDbContext db)
        {
            Console.WriteLine("\nHomeworks: ");
            var homeworkCompleted = db.Homeworks.Where(h => h.StudentId.HasValue).FirstOrDefault();
            Console.WriteLine(
                " - {0} ({1}) -> Completed by:  {2}",
                 homeworkCompleted.Content,
                 homeworkCompleted.Course.Description,
                 homeworkCompleted.Student.FullName);
        }
    }
}
