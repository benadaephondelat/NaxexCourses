namespace ServiceLayer
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Models;
    using Interfaces;
    using DataLayer.Interfaces;
    using Exceptions.CourseException;
    using Exceptions.UserExceptions;

    public class CoursesService : ICoursesService
    {
        private ICoursesData data;

        public CoursesService(ICoursesData data)
        {
            this.data = data;
        }

        public async Task CreateNewCourse(string courseName, double coursePoints, string username)
        {
            var currentUser = this.GetUserByUsername(username);

            if (currentUser == null)
            {
                throw new UserNotFoundException();
            }

            this.IfCourseNameExistsThrowException(courseName);

            Course newCourse = new Course();

            newCourse.CourseName = courseName;
            newCourse.CoursePoints = coursePoints;
            newCourse.CourseCreator = currentUser;
            newCourse.CourseCreatorId = currentUser.Id;

            this.data.Courses.Add(newCourse);

            await this.data.SaveChangesAsync();
        }

        private ApplicationUser GetUserByUsername(string username)
        {
            var user = this.data.Users.All().FirstOrDefault(u => u.UserName == username);

            return user;
        }

        private void IfCourseNameExistsThrowException(string courseName)
        {
            bool isDuplicateCourse = this.data.Courses.All().Any(c => c.CourseName.Equals(courseName, StringComparison.OrdinalIgnoreCase));

            if (isDuplicateCourse)
            {
                throw new CourseAlreadyExistsException();
            }
        }
    }
}