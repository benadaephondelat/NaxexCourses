﻿namespace Courses.Web.Controllers
{
    using System.Web.Mvc;

    using Constants;
    using ServiceLayer.Interfaces;
    using Models.ManageCourses;
    using System.Threading.Tasks;
    using FrameworkExtentions;
    using System.Collections.Generic;
    using global::Models;
    using AutoMapper;

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
    }
}