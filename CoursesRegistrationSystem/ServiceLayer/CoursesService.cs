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
    using System.Collections.Generic;

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

        public void DeleteCourseById(int id, string username)
        {
            var course = this.data.Courses.All().FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                throw new CourseNotFoundException();
            }

            var user = this.GetUserByUsername(username);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (course.CourseCreatorId != user.Id)
            {
                throw new UserNotAuthorizedException();
            }

            user.Courses.Remove(course);

            this.data.Courses.Delete(course);

            this.data.SaveChanges();
        }

        public IEnumerable<Course> GetUserCreatedCourses(string username)
        {
            var currentUser = this.GetUserByUsername(username);

            if (currentUser == null)
            {
                throw new UserNotFoundException();
            }

            var courses = this.data.Courses.All().Where(c => c.CourseCreatorId == currentUser.Id).AsEnumerable();

            return courses;
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