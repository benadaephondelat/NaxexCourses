namespace Courses.ServiceLayer.Tests
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
    }
}