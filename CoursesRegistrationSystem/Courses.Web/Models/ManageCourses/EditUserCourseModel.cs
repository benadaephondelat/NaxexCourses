using Models;
using Courses.Web.FrameworkExtentions.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Courses.Web.Models.ManageCourses
{
    public class EditUserCourseModel : IMapFrom<Course>, IMapTo<Course>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string CourseName { get; set; }

        [Required]
        [Range(0, 10)]
        public double CoursePoints { get; set; }
    }
}