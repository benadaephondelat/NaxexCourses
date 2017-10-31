namespace ServiceLayer
{
    using System;
    using System.Collections.Generic;

    using Models;
    using Interfaces;
    using DataLayer.Interfaces;

    public class CoursesService : ICoursesService
    {
        private ICoursesData data;

        public CoursesService(ICoursesData data)
        {
            this.data = data;
        }

        public IEnumerable<Course> GetAllRegisteredCoursesByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}