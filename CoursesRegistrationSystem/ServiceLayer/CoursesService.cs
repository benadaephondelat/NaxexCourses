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

        public Course CreateNewCourse(string courseName, double coursePoints, string username)
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
            newCourse.CourseCreatorId = currentUser.Id;

            this.data.Courses.Add(newCourse);
            this.data.SaveChanges();

            return newCourse;
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

            //TODO CHECK FOR DUPLICATE COURSE NAME

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

            var courses = this.data.Courses.All().AsParallel();
                                        
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
                                           .Where(c => c.ApplicationUsers.Any(s => s.Id == user.Id));

            var allCourses = this.data.Courses.All().ToList();

            //var firstCourse = allCourses[0];

            //var firstCourseRegistered = firstCourse.RegisteredStudents.ToList();

            //var secondCourse = allCourses[1];

            //var secondCourseRegistered = secondCourse.RegisteredStudents.ToList();
                                   
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

        public double GetUserCurrentPoints(string username)
        {
            var user = this.GetUserByUsername(username);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user.CurrentStudentPoints;
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
        }

        public void UnregisterFromCourse(int courseId, string username)
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