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
        /// <returns>Task</returns>
        Task CreateNewCourse(string courseName, double coursePoints, string username);
    }
}