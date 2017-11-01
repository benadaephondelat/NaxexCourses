namespace Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        public Course()
        {
            this.ApplicationUsers = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string CourseName { get; set; }

        [Required]
        [Range(0, 10)]
        public double CoursePoints { get; set; }

        public string CourseCreatorId { get; set; }

        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}