namespace ServiceLayer.LinqExtentions
{
    using Models;
    using System.Linq;
    using System.Collections.Generic;
    using DataLayer.Interfaces;

    public static class LinqExtentions
    {
        /// <summary>
        /// Returns a list of all courses to which the user is registered
        /// </summary>
        /// <param name="source">List of Courses</param>
        /// <returns>IEnumerable<Course></Course></returns>
        public static IEnumerable<Course> GetAllUserRegisteredCourses(this IGenericRepository<Course> source, string userId)
        {
            var result = source.All()
                               .Where(c => c.ApplicationUsers.Any(s => s.Id == userId))
                               .AsEnumerable();

            return result;
        }
    }
}