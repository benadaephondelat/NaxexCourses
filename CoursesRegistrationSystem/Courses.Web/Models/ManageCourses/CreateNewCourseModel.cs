namespace Courses.Web.Models.ManageCourses
{
    using System.ComponentModel.DataAnnotations;

    public class CreateNewCourseModel
    {
        private const int CourseNameMinLenght = 3;
        private const int CourseNameMaxLenght = 50;
        private const int CoursePointsMinRange = 0;
        private const int CoursePointsMaxRange = 10;

        [Required]
        [MinLength(CourseNameMinLenght), MaxLength(CourseNameMaxLenght)]
        public string CourseName { get; set; }

        [Required]
        [Range(CoursePointsMinRange, CoursePointsMaxRange)]
        public double CoursePoints { get; set; }
    }
}