namespace Courses.Web.Controllers
{
    using System.Web.Mvc;

    using Constants;
    using ServiceLayer.Interfaces;
    using Models.ManageCourses;
    using System.Threading.Tasks;
    using FrameworkExtentions;
    using System.Collections.Generic;
    using AutoMapper;
    using global::Models;

    public class ManageCoursesController : BaseController
    {
        private readonly ICoursesService coursesService;

        public ManageCoursesController()
        {
        }

        public ManageCoursesController(ICoursesService coursesService)
        {
            this.coursesService = coursesService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(WebConstants.IndexView);
        }

        [HttpGet]
        public ActionResult CreateNewCourse()
        {
            CreateNewCourseModel model = new CreateNewCourseModel();

            return View(WebConstants.CreateNewCourseView, model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [CheckModelStateFilter]
        public async Task<ActionResult> CreateNewCourse(CreateNewCourseModel model)
        {
            await this.coursesService.CreateNewCourse(model.CourseName, model.CoursePoints, base.CurrentUserName());

            return RedirectToAction(WebConstants.IndexView);
        }

        [HttpGet]
        public ActionResult EditCourses()
        {
            string username = base.CurrentUserName();

            IEnumerable<Course> userCourses = this.coursesService.GetUserCreatedCourses(username);

            IEnumerable<EditCourseGridViewModel> result = Mapper.Map<IEnumerable<EditCourseGridViewModel>>(userCourses);

            return View(WebConstants.EditCoursesView, result);
        }

        [HttpPost, ValidateAntiForgeryTokenAjax]
        public ActionResult DeleteCourse(int courseId)
        {
            string username = base.CurrentUserName();

            this.coursesService.DeleteCourseById(courseId, username);

            return Json(true);
        }

        [HttpPost, ValidateAntiForgeryTokenAjax]
        public ActionResult EditCourse(int courseId)
        {
            var result = new
            {
                result = "Redirect",
                url = Url.Action("EditUserCourse", "ManageCourses",
                new { @courseId = courseId })
            };

            return Json(result);
        }

        [HttpGet]
        public ActionResult EditUserCourse(int courseId)
        {
            string username = base.CurrentUserName();

            Course course = this.coursesService.GetCourseById(courseId, username);

            EditUserCourseModel model = Mapper.Map<EditUserCourseModel>(course);

            return View("EditUserCourse", model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [CheckModelStateFilter]
        public ActionResult EditUserCourse(EditUserCourseModel model)
        {
            string username = base.CurrentUserName();

            this.coursesService.EditCourseById(model.Id, model.CourseName, model.CoursePoints, username);

            return RedirectToAction("EditCourses");
        }
    }
}