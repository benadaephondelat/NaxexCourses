﻿namespace Courses.ServiceLayer.Tests
{
    using global::ServiceLayer;
    using global::ServiceLayer.Interfaces;

    using Helpers;
    using DataLayer.Interfaces;
    using Exceptions.UserExceptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using Exceptions.CourseException;
    using System.Linq;
    using Models;

    [TestClass]
    public class CoursesServiceTests
    {
        private Mock<ICoursesData> dataLayerMock;
        private ICoursesService coursesService;
        private DataLayerMockHelper mockHelper;

        [TestInitialize]
        public void SetUp()
        {
            this.mockHelper = new DataLayerMockHelper();

            this.dataLayerMock = this.mockHelper.SetupTicTacToeDataMock();

            this.coursesService = new CoursesService(dataLayerMock.Object);
        }

        #region CreateNewCourse Tests

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void CreateNewCourse_Should_Throw_UserNotFoundException_If_No_Such_Users_Exists()
        {
            this.coursesService.CreateNewCourse(MockConstants.UniqueCourseName, MockConstants.MaximumAllowedCoursePoints, MockConstants.InvalidUserUsername);
        }

        [TestMethod]
        [ExpectedException(typeof(CourseAlreadyExistsException))]
        public void CreateNewCourse_Should_Throw_CourseAlreadyExistsException_If_Course_With_That_Name_Exists()
        {
            this.coursesService.CreateNewCourse(MockConstants.EmptyCourseName, MockConstants.MaximumAllowedCoursePoints, MockConstants.MinPointsUserUsername);
        }

        [TestMethod]
        public void CreateNewCourse_Should_Save_The_Data_Properly()
        {
            string newCourseName = "newCourse";

            Course course = this.coursesService.CreateNewCourse(newCourseName, MockConstants.MaximumAllowedCoursePoints, MockConstants.MinPointsUserUsername);

            Assert.AreEqual(newCourseName, course.CourseName);
            Assert.AreEqual(MockConstants.MaximumAllowedCoursePoints, course.CoursePoints);
            Assert.AreEqual(MockConstants.MinPointsUserId, course.CourseCreatorId);
        }

        [TestMethod]
        public void CreateNewCourse_Should_Call_CoursesRepository_Add_Method_Once()
        {
            var addCourseCounter = 0;

            this.dataLayerMock.Setup(x => x.Courses.Add(It.IsAny<Course>())).Callback(() => addCourseCounter++);

            this.coursesService.CreateNewCourse("newCourse", MockConstants.MaximumAllowedCoursePoints, MockConstants.MinPointsUserUsername);

            this.dataLayerMock.Verify(x => x.Courses.Add(It.IsAny<Course>()), Times.Exactly(1));

            Assert.AreEqual(1, addCourseCounter);
        }

        [TestMethod]
        public void CreateNewCourse_Should_Call_SaveChanges_Method_Once()
        {
            var saveChangesCounter = 0;

            this.dataLayerMock.Setup(x => x.SaveChanges()).Callback(() => saveChangesCounter++);

            this.coursesService.CreateNewCourse("newCourse", MockConstants.MaximumAllowedCoursePoints, MockConstants.MinPointsUserUsername);

            this.dataLayerMock.Verify(x => x.SaveChanges(), Times.Once());

            Assert.AreEqual(1, saveChangesCounter);
        }

        #endregion

        #region GetUserCreatedCourses Tests

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void GetUserCreatedCourses_Should_Throw_UserNotFoundException_If_No_Such_Users_Exists()
        {
            this.coursesService.GetUserCreatedCourses(MockConstants.InvalidUserUsername);
        }

        [TestMethod]
        public void GetUserCreatedCourses_Should_Return_Two_Courses()
        {
            var result = this.coursesService.GetUserCreatedCourses(MockConstants.MinPointsUserUsername);

            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region DeleteCourseById Tests

        [TestMethod]
        [ExpectedException(typeof(CourseNotFoundException))]
        public void DeleteCourseById_Should_Throw_CourseNotFoundException_If_No_Such_Course_Exists()
        {
            this.coursesService.DeleteCourseById(666, MockConstants.MinPointsUserUsername);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void DeleteCourseById_Should_Throw_UserNotFoundException_If_No_Such_User_Exists()
        {
            this.coursesService.DeleteCourseById(MockConstants.EmptyCourseId, MockConstants.InvalidUserUsername);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotAuthorizedException))]
        public void DeleteCourseById_Should_Throw_UserNotAuthorizedException_If_The_Creator_Of_The_Course_Is_Not_The_Requesting_User()
        {
            this.coursesService.DeleteCourseById(MockConstants.EmptyCourseId, MockConstants.MaxPointsUserUsername);
        }

        [Ignore] //Investigate why the course is missing when calling the service, it's mocked properly(i think)
        [TestMethod]
        public void DeleteCourseById_Should_Call_CoursesRepository_Delete_Method_Once()
        {
            var deleteCourseCounter = 0;

            this.dataLayerMock.Setup(x => x.Courses.Delete(It.IsAny<Course>())).Callback(() => deleteCourseCounter++);

            this.coursesService.DeleteCourseById(MockConstants.EmptyCourseId, MockConstants.MinPointsUserUsername);

            this.dataLayerMock.Verify(x => x.Courses.Delete(It.IsAny<Course>()), Times.Exactly(1));

            Assert.AreEqual(1, deleteCourseCounter);
        }

        [TestMethod]
        public void DeleteCourseById_Should_Call_SaveChanges_Method_Once()
        {
            var saveChangesCounter = 0;

            this.dataLayerMock.Setup(x => x.SaveChanges()).Callback(() => saveChangesCounter++);

            this.coursesService.DeleteCourseById(MockConstants.EmptyCourseId, MockConstants.MinPointsUserUsername);

            this.dataLayerMock.Verify(x => x.SaveChanges(), Times.Once());

            Assert.AreEqual(1, saveChangesCounter);
        }

        #endregion

        #region GetCourseById

        [TestMethod]
        [ExpectedException(typeof(CourseNotFoundException))]
        public void GetCourseById_Should_Throw_CourseNotFoundException_If_No_Such_Course_Exists()
        {
            Course course = this.coursesService.GetCourseById(MockConstants.InvalidCourseId, MockConstants.MinPointsUserUsername);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void GetCourseById_Should_Throw_UserNotFoundException_If_No_Such_User_Exists()
        {
            Course course = this.coursesService.GetCourseById(MockConstants.EmptyCourseId, MockConstants.InvalidUserUsername);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotAuthorizedException))]
        public void GetCourseById_Should_Throw_UserNotAuthorizedException_If_The_Creator_Of_The_Course_Is_Not_The_Requesting_User()
        {
            Course course = this.coursesService.GetCourseById(MockConstants.EmptyCourseId, MockConstants.MaxPointsUserUsername);
        }

        [TestMethod]
        public void GetCourseById_Should_Return_Course_When_Data_Is_Valid()
        {
            Course course = this.coursesService.GetCourseById(MockConstants.EmptyCourseId, MockConstants.MinPointsUserUsername);

            Assert.IsNotNull(course);
        }

        #endregion

        #region EditCourseById

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void EditCourseById_Should_Throw_UserNotFoundException_If_No_Such_User_Exists()
        {
            this.coursesService.EditCourseById(MockConstants.EmptyCourseId, "newCourseName", 10D, MockConstants.InvalidUserUsername);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotAuthorizedException))]
        public void EditCourseById_Should_Throw_UserNotAuthorizedException_If_The_Requesting_User_Is_Not_The_Course_Creator()
        {
            this.coursesService.EditCourseById(MockConstants.EmptyCourseId, "newCourseName", 10D, MockConstants.MaxPointsUserUsername);
        }

        [TestMethod]
        [ExpectedException(typeof(CourseNotFoundException))]
        public void EditCourseById_Should_Throw_CourseNotFoundException_If_No_Such_Course_Exists()
        {
            this.coursesService.EditCourseById(MockConstants.InvalidCourseId, "newCourseName", 10D, MockConstants.MinPointsUserId);
        }

        [TestMethod]
        [ExpectedException(typeof(CourseAlreadyExistsException))]
        public void EditCourseById_Should_Throw_CourseAlreadyExistsException_If_There_Is_A_Course_With_The_Same_Name()
        {
            this.coursesService.EditCourseById(MockConstants.EmptyCourseWithFivePointsId, MockConstants.EmptyCourseName, 10D, MockConstants.MinPointsUserUsername);
        }

        [TestMethod]
        public void EditCourseById_Should_Edit_Course_Properties_If_Data_Is_Valid()
        {
            string newCourseName = "newCourseName";
            double newCoursePoints = 1.5D;

            Course course = this.coursesService.EditCourseById(MockConstants.EmptyCourseWithFivePointsId, newCourseName, newCoursePoints, MockConstants.MinPointsUserUsername);

            Assert.AreEqual(newCourseName, course.CourseName);
            Assert.AreEqual(newCoursePoints, course.CoursePoints);
        }

        [Ignore] //Investigate why the course is missing when calling the service, it's mocked properly(i think)
        [TestMethod]
        public void EditCourseById_Should_Call_CoursesRepository_Update_Method_Once()
        {
            var updateCourseCounter = 0;

            this.dataLayerMock.Setup(x => x.Courses.Update(It.IsAny<Course>())).Callback(() => updateCourseCounter++);

            this.coursesService.EditCourseById(MockConstants.EmptyCourseWithFivePointsId, "newCourseName", 1.5D, MockConstants.MinPointsUserUsername);

            this.dataLayerMock.Verify(x => x.Courses.Update(It.IsAny<Course>()), Times.Exactly(1));

            Assert.AreEqual(1, updateCourseCounter);
        }

        [TestMethod]
        public void EditCourseById_Should_Call_SaveChanges_Method_Once()
        {
            var saveChangesCounter = 0;

            this.dataLayerMock.Setup(x => x.SaveChanges()).Callback(() => saveChangesCounter++);

            this.coursesService.EditCourseById(MockConstants.EmptyCourseWithFivePointsId, "newCourseName", 1.5D, MockConstants.MinPointsUserUsername);

            this.dataLayerMock.Verify(x => x.SaveChanges(), Times.Once());

            Assert.AreEqual(1, saveChangesCounter);
        }

        #endregion
    }
}