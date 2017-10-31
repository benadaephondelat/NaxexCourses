namespace ServiceLayer.Interfaces
{
    using Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
        /// <returns>Task</returns>
        Task CreateNewCourse(string courseName, double coursePoints, string username);

        /// <summary>
        /// Get all courses created by the user or throws an exception
        /// </summary>
        /// <param name="username">username of the current user</param>
        /// <exception cref="UserNotFoundException"></exception>
        /// <returns>Task<IEnumerable<Course></Course>></returns>
        IEnumerable<Course> GetUserCreatedCourses(string username);
    }
}