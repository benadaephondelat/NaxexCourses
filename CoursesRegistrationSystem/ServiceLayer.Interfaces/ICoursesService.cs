namespace ServiceLayer.Interfaces
{
    using Models;
    using System.Collections.Generic;

    public interface ICoursesService
    {
        /// <summary>
        /// Returns a list of all courses to which a user is registered
        /// </summary>
        /// <param name="username">Username of the user</param>
        /// <returns>IEnumerable<Course></Course></returns>
        IEnumerable<Course> GetAllRegisteredCoursesByUsername(string username);
    }
}