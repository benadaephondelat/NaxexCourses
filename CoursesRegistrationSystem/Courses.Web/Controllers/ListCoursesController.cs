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

    [CheckIfLoggedInFilter, CheckIfUserSessionIsExpired("Account", "Login")]
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

            double currentUserPoints = this.coursesService.GetUserCurrentPoints(username);

            return View(WebConstants.IndexView, currentUserPoints);
        }

        [HttpGet]
        public ActionResult GetAvailableCourses()
        {
            string username = base.CurrentUserName();

            IEnumerable<Course> availableCourses = this.coursesService.GetAllAvailableCourses(username);

            IEnumerable<CourseListViewModel> viewModel = Mapper.Map<IEnumerable<CourseListViewModel>>(availableCourses);

            return PartialView("_AvailableCourses", viewModel);
        }

        [HttpGet]
        public ActionResult GetRegisteredCourses()
        {
            string username = base.CurrentUserName();

            IEnumerable<Course> registeredCourses = this.coursesService.GetAllRegisteredCourses(username);

            IEnumerable<CourseListViewModel> viewModel = Mapper.Map<IEnumerable<CourseListViewModel>>(registeredCourses);

            return PartialView("_RegisteredCourses", viewModel);
        }

        [HttpPost, ValidateAntiForgeryTokenAjax]
        [AlreadyRegisteredExceptionHandler, MaxedCoursePointsEceptionHandler]
        public ActionResult RegisterToCourse(int courseId)
        {
            string username = base.CurrentUserName();

            this.coursesService.RegisterToCourse(courseId, username);

            return Json(true);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult UnregisterFromCourse(int courseId)
        {
            string username = base.CurrentUserName();

            this.coursesService.UnregisterFromCourse(courseId, username);

            return RedirectToAction("Index");
        }
    }
}