namespace Courses.Web.Controllers
{
    using FrameworkExtentions;
    using ServiceLayer.Interfaces;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using global::Models;
    using Models.ListCourses;
    using AutoMapper;
    using Constants;

    [CheckIfLoggedInFilter]
    public class ListCoursesController : BaseController
    {
        private readonly ICoursesService coursesService;

        public ListCoursesController()
        {
        }

        public ListCoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            string username = base.CurrentUserName();

            IEnumerable<Course> availableCourses = this.coursesService.GetAllAvailableCourses(username);

            IEnumerable<Course> registeredCourses = this.coursesService.GetAllRegisteredCourses(username);

            CoursesListViewModel viewModel = new CoursesListViewModel();

            viewModel.AvailableCourses = Mapper.Map<IEnumerable<CourseListViewModel>>(availableCourses);

            viewModel.RegisteredCourses = Mapper.Map<IEnumerable<CourseListViewModel>>(registeredCourses);

            //TODO Add available points to the view model
            //var user = this.coursesService.GetUserByUsername(username);
            //viewModel.AvailablePoints = user.AvailablePoints;

            return View(WebConstants.IndexView, viewModel);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult RegisterToCourse(int courseId)
        {
            string username = base.CurrentUserName();

            this.coursesService.RegisterToCourse(courseId, username);

            return RedirectToAction(WebConstants.IndexView);
        }
    }
}