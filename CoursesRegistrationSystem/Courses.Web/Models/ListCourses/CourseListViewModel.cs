using Models;
using Courses.Web.FrameworkExtentions.Mappings;

namespace Courses.Web.Models.ListCourses
{
    public class CourseListViewModel : IMapFrom<Course>
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public double CoursePoints { get; set; }
    }
}