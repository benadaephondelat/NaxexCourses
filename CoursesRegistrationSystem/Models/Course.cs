namespace Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        private ICollection<ApplicationUser> registeredStudents;

        public Course()
        {
            this.RegisteredStudents = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string CourseName { get; set; }

        [Required]
        [Range(0, 10)]
        public double CoursePoints { get; set; }

        public virtual ICollection<ApplicationUser> RegisteredStudents
        {
            get
            {
                return this.registeredStudents;
            }

            set
            {
                this.registeredStudents = value;
            }
        }
    }
}
