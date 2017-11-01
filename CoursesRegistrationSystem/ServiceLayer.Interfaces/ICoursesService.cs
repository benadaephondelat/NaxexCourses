namespace ServiceLayer.Interfaces
{
    using Models;
    using System.Collections.Generic;

    public interface ICoursesService
    {
        /// <summary>
        /// Creates a new Course or throws an exception
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="coursePoints"></param>
        /// <param name="username"></param>
        /// <exception cref="CourseAlreadyExistsException"></exception>
        /// <exception cref="UserNotFoundException"></exception>
        void CreateNewCourse(string courseName, double coursePoints, string username);

        /// <summary>
        /// Get all courses created by the user or throws an exception
        /// </summary>
        /// <param name="username">username of the current user</param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <returns>Task<IEnumerable<Course></Course>></returns>
        IEnumerable<Course> GetUserCreatedCourses(string username);

        /// <summary>
        /// Deletes a course by a given id or throws an exception
        /// </summary>
        /// <param name="courseId">id of the course to delete</param>
        /// <param name="username">the username of the current user</param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="UserNotAuthorizedException"></exception>
        /// <exception cref="CourseNotFoundException"></exception>
        void DeleteCourseById(int courseId, string username);

        /// <summary>
        /// Returns a course by a given id or throws an exception
        /// </summary>
        /// <param name="courseId">id of the course</param>
        /// <param name="username">username of the current user</param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="UserNotAuthorizedException"></exception>
        /// <exception cref="CourseNotFoundException"></exception>
        /// <returns>Course</returns>
        Course GetCourseById(int courseId, string username);

        /// <summary>
        /// Edits a course by a given Id and properties or throws an exception
        /// </summary>
        /// <param name="courseId">id of the course</param>
        /// <param name="courseName">name of the course</param>
        /// <param name="coursePoints">points of the course</param>
        /// <param name="username">username of the current user</param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="UserNotAuthorizedException"></exception>
        /// <exception cref="CourseNotFoundException"></exception>
        /// <exception cref="CourseAlreadyExistsException"></exception>
        void EditCourseById(int courseId, string courseName, double coursePoints, string username);

        /// <summary>
        /// Returns all courses to which the user has not been registered or throws an exception
        /// </summary>
        /// <param name="username">username of the current user</param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <returns>IEnumerable<Course></Course></returns>
        IEnumerable<Course> GetAllAvailableCourses(string username);

        /// <summary>
        /// Returns all courses to which the user has been registered or throws an exception
        /// </summary>
        /// <param name="username">username of the current user</param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <returns>IEnumerable<Course></Course></returns>
        IEnumerable<Course> GetAllRegisteredCourses(string username);

        /// <summary>
        /// Registers a user to a given course or throws an exception
        /// </summary>
        /// <param name="courseId">id of the course to register to</param>
        /// <param name="username">username of the current user</param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="CourseNotFoundException"></exception>
        /// <exception cref="AlreadyRegisteredToCourseException"></exception>
        /// <exception cref="MaxedCoursePointsException"></exception>
        void RegisterToCourse(int courseId, string username);

        /// <summary>
        /// Unregisters a user to a given course or throws an exception
        /// </summary>
        /// <param name="courseId">id of the course to register to</param>
        /// <param name="username">username of the current user</param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <exception cref="CourseNotFoundException"></exception>
        /// <exception cref="NotRegisteredToCourseException"></exception>
        /// <exception cref="InsufficientCoursePointsException"></exception>
        void UnregisterFromCourse(int courseId, string username);

        /// <summary>
        /// Returns the current points to a given user or throws an exception
        /// </summary>
        /// <param name="username">username of the current user</param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <returns>double</returns>
        double GetUserCurrentPoints(string username);
    }
}