namespace ServiceLayer
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Models;
    using Interfaces;
    using LinqExtentions;
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

        public Course CreateNewCourse(string courseName, double coursePoints, string username)
        {
            ApplicationUser currentUser = this.GetUserByUsername(username);

            this.IfCourseNameExistsThrowException(courseName);

            Course newCourse = new Course();

            newCourse.CourseName = courseName;
            newCourse.CoursePoints = coursePoints;
            newCourse.CourseCreatorId = currentUser.Id;

            this.data.Courses.Add(newCourse);
            this.data.SaveChanges();

            return newCourse;
        }

        public void DeleteCourseById(int id, string username)
        {
            ApplicationUser user = this.GetUserByUsername(username);

            Course course = this.GetCourseById(id);

            this.IfUserIsNotCreatorOfTheCourseThrowException(user.Id, course.CourseCreatorId);

            this.data.Courses.Delete(course);
            this.data.SaveChanges();
        }

        public Course EditCourseById(int courseId, string courseName, double coursePoints, string username)
        {
            Course course = this.GetCourseById(courseId);

            ApplicationUser user = this.GetUserByUsername(username);

            this.IfUserIsNotCreatorOfTheCourseThrowException(user.Id, course.CourseCreatorId);
            this.IfCourseNameExistsThrowException(courseName);

            course.CourseName = courseName;
            course.CoursePoints = coursePoints;

            this.data.Courses.Update(course);
            this.data.SaveChanges();

            return course;
        }

        public IEnumerable<Course> GetAllAvailableCourses(string username)
        {
            ApplicationUser user = this.GetUserByUsername(username);

            var courses = this.data.Courses.All().AsParallel();
                                        
            return courses.AsEnumerable();
        }

        public IEnumerable<Course> GetAllRegisteredCourses(string username)
        {
            ApplicationUser user = this.GetUserByUsername(username);

            var courses = this.data.Courses.GetAllUserRegisteredCourses(user.Id);

            return courses.AsEnumerable();
        }

        public Course GetCourseById(int courseId, string username)
        {
            Course course = this.GetCourseById(courseId);

            ApplicationUser user = this.GetUserByUsername(username);

            this.IfUserIsNotCreatorOfTheCourseThrowException(user.Id, course.CourseCreatorId);

            return course;
        }

        public IEnumerable<Course> GetUserCreatedCourses(string username)
        {
            ApplicationUser currentUser = this.GetUserByUsername(username);

            var courses = this.data.Courses.All().Where(c => c.CourseCreatorId == currentUser.Id).AsEnumerable();

            return courses;
        }

        public double GetUserCurrentPoints(string username)
        {
            ApplicationUser user = this.GetUserByUsername(username);

            return user.CurrentStudentPoints;
        }

        public Course RegisterToCourse(int courseId, string username)
        {
            Course course = this.GetCourseById(courseId);

            ApplicationUser currentUser = this.GetUserByUsername(username);

            bool isUserAlreadyRegistered = currentUser.Courses.Any(c => c.Id == course.Id);

            if (isUserAlreadyRegistered)
            {
                throw new AlreadyRegisteredToCourseException();
            }

            if ((currentUser.CurrentStudentPoints + course.CoursePoints) > currentUser.StudentMaxPoints)
            {
                throw new MaxedCoursePointsException();
            }

            course.ApplicationUsers.Add(currentUser);
            this.data.Courses.Update(course);

            currentUser.Courses.Add(course);
            currentUser.CurrentStudentPoints += course.CoursePoints;
            this.data.Users.Update(currentUser);

            this.data.SaveChanges();

            return course;
        }

        public Course UnregisterFromCourse(int courseId, string username)
        {
            Course course = this.GetCourseById(courseId);

            ApplicationUser currentUser = this.GetUserByUsername(username);

            bool isUserRegistered = currentUser.Courses.Any(c => c.Id == course.Id);

            if (isUserRegistered == false)
            {
                throw new NotRegisteredToCourseException();
            }

            if ((currentUser.CurrentStudentPoints - course.CoursePoints) < 0D)
            {
                throw new InsufficientCoursePointsException();
            }

            course.ApplicationUsers.Remove(currentUser);
            this.data.Courses.Update(course);

            currentUser.Courses.Remove(course);
            currentUser.CurrentStudentPoints -= course.CoursePoints;
            this.data.Users.Update(currentUser);

            this.data.SaveChanges();

            return course;
        }

        /// <summary>
        /// Returns a user or throws UserNotFoundException if the user is not found
        /// </summary>
        /// <param name="username">username</param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <returns>ApplicationUser</returns>
        private ApplicationUser GetUserByUsername(string username)
        {
            ApplicationUser user = this.data.Users.All().FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        /// <summary>
        /// Returns a Course by a given id or throws CourseNotFoundException
        /// </summary>
        /// <param name="id">id of the course</param>
        /// <exception cref="CourseNotFoundException"></exception>
        /// <returns>Course</returns>
        private Course GetCourseById(int id)
        {
            Course course = this.data.Courses.All().FirstOrDefault(c => c.Id == id);

            if (course == null)
            {
                throw new CourseNotFoundException();
            }

            return course;
        }

        /// <summary>
        /// If both strings are not the same throw exception
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="courseCreatorId">Id of the course creator</param>
        /// <exception cref="UserNotAuthorizedException"></exception>
        private void IfUserIsNotCreatorOfTheCourseThrowException(string userId, string courseCreatorId)
        {
            if (userId != courseCreatorId)
            {
                throw new UserNotAuthorizedException();
            }
        }

        /// <summary>
        /// If there is a course with that name in the database throws exception
        /// </summary>
        /// <exception cref="CourseAlreadyExistsException"></exception>
        /// <param name="courseName">name of the course</param>
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