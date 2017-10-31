using System;
using System.Linq;
using Courses.Web.FrameworkExtentions.Mappings;
using Models;
using AutoMapper;

namespace Courses.Web.Models.ManageCourses
{
    public class EditCourseGridViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string CourseName { get; set; }

        public double CoursePoints { get; set; }

        public int RegisteredStudents { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Course, EditCourseGridViewModel>()
                         .ForMember(vm => vm.RegisteredStudents, opt => opt.MapFrom(s => s.RegisteredStudents.Count()));
        }
    }
}