namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using StudentSystem.Models;

    public sealed class Configuration : DbMigrationsConfiguration<StudentSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = "StudentSystem.Data.StudentSystemDbContext";
        }

        protected override void Seed(StudentSystemDbContext context)
        {
            this.SeedCourses(context);
            this.SeedStudents(context);
            this.SeedHomeworks(context);
        }

        private void SeedStudents(StudentSystemDbContext context)
        {
            if (context.Students.Any())
            {
                return;
            }

            context.Students.Add(new Student
            {
                FirstName = "Ivan",
                LastName = "Ivanov",
                Age = 21
            });

            context.Students.Add(new Student
            {
                FirstName = "Georgi",
                LastName = "Georgiev",
                Age = 17,
                Courses = context.Courses.OrderByDescending(c => c.Name).Skip(1).Take(1).ToList()
            });

            context.SaveChanges();
        }

        private void SeedCourses(StudentSystemDbContext context)
        {
            if (context.Courses.Any())
            {
                return;
            }

            context.Courses.Add(new Course
            {
                Name = "Databases",
                Description = "Database course in Telerik Academy",
                Homeworks = new List<Homework>()
                {
                    new Homework
                    {
                        Content = "Processing Json in .NET"
                    },
                    new Homework
                    {
                        Content = "Entity Framework"
                    },
                    new Homework
                    {
                        Content = "ADO.NET"
                    }
                }
            });

            context.Courses.Add(new Course
            {
                Name = "HQC",
                Description = "HQC course in Telerik Academy",
                Homeworks = new List<Homework>()
                {
                    new Homework
                    {
                        Content = "Refactoring"
                    },
                    new Homework
                    {
                        Content = "Debugging tools"
                    },
                    new Homework
                    {
                        Content = "Design Patterns"
                    }
                }
            });

            context.Courses.Add(new Course
                {
                    Name = "JavaScript",
                    Description = "JavaScript course in Telerik Academy"
                });

            context.Courses.Add(new Course
            {
                Name = "PHP",
                Description = "Not available"
            });

            context.SaveChanges();
        }

        private void SeedHomeworks(StudentSystemDbContext context)
        {
            var firstStudent = context.Students.FirstOrDefault();
            var someHomework = context.Courses.FirstOrDefault().Homeworks.FirstOrDefault();

            firstStudent.Homeworks.Add(new Homework
            {
                Content = someHomework.Content,
                TimeSent = DateTime.Now,
                Course = someHomework.Course
            });

            context.SaveChanges();
        }
    }
}
