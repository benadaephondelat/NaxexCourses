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

        public void EditCourseById(int courseId, string courseName, double coursePoints, string username)
        {
            var course = this.data.Courses.All().FirstOrDefault(c => c.Id == courseId);

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

            course.CourseName = courseName;
            course.CoursePoints = coursePoints;

            this.data.Courses.Update(course);
            this.data.SaveChanges();
        }

        public IEnumerable<Course> GetAllAvailableCourses(string username)
        {
            var user = this.GetUserByUsername(username);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var courses = this.data.Courses
                                   .All()
                                   .Where(c => c.RegisteredStudents.Any(s => s.Id != user.Id));
                                        
            return courses.AsEnumerable();
        }

        public IEnumerable<Course> GetAllRegisteredCourses(string username)
        {
            var user = this.GetUserByUsername(username);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var courses = this.data.Courses.All()
                                           .Where(c => c.RegisteredStudents.Any(s => s.Id == user.Id));
                                   
            return courses.AsEnumerable();
        }

        public Course GetCourseById(int courseId, string username)
        {
            var course = this.data.Courses.All().FirstOrDefault(c => c.Id == courseId);

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

            return course;
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

        public void RegisterToCourse(int courseId, string username)
        {
            var course = this.data.Courses.All().FirstOrDefault(c => c.Id == courseId);

            if (course == null)
            {
                throw new CourseNotFoundException();
            }

            var currentUser = this.GetUserByUsername(username);

            if (currentUser == null)
            {
                throw new UserNotFoundException();
            }

            bool isUserAlreadyRegistered = course.RegisteredStudents.Any(s => s.Id == currentUser.Id);

            if (isUserAlreadyRegistered)
            {
                throw new AlreadyRegisteredToCourseException();
            }

            if (currentUser.CurrentStudentPoints < course.CoursePoints)
            {
                throw new InsufficientCoursePointsException();
            }

            course.RegisteredStudents.Add(currentUser);
            this.data.Courses.Update(course);

            currentUser.CurrentStudentPoints -= course.CoursePoints;
            this.data.Users.Update(currentUser);

            this.data.SaveChanges();
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