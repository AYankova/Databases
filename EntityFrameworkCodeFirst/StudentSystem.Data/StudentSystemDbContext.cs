namespace StudentSystem.Data
{
    using System.Data.Entity;
    using Migrations;
    using Models;

    public class StudentSystemDbContext : DbContext
    {
        public StudentSystemDbContext()
            : base("StudentSystemDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemDbContext, Configuration>());
        }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Homework> Homeworks { get; set; }
    }
}
