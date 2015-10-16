namespace StudentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Course
    {
        private ICollection<Student> students;
        private ICollection<Homework> homeworks;

        public Course()
        {
            this.students = new HashSet<Student>();
            this.homeworks = new HashSet<Homework>();
            this.Materials = new HashSet<string>();
        }

        [Key]
        public int CourseId { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Column("Course Name")]
        public string Name { get; set; }

        [Column("Course Description")]
        public string Description { get; set; }

        public ICollection<string> Materials { get; set; }

        public virtual ICollection<Student> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }
    }
}
