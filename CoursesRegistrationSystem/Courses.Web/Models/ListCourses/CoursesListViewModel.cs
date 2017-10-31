namespace Courses.Web.Models.ListCourses
{
    using System.Collections.Generic;

    public class CoursesListViewModel
    {
        public IEnumerable<CourseListViewModel> AvailableCourses { get; set; }

        public IEnumerable<CourseListViewModel> RegisteredCourses { get; set; }
    }
}