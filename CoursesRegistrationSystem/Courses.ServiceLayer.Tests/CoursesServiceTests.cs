namespace Courses.ServiceLayer.Tests
{
    using global::ServiceLayer;
    using global::ServiceLayer.Interfaces;

    using Helpers;
    using DataLayer.Interfaces;
    using Exceptions.UserExceptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;

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

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void CreateNewCourse_Should_Throw_UserNotFoundException_If_No_Such_Users_Exists_In_The_Database()
        {
            this.coursesService.CreateNewCourse(MockConstants.InvalidUserId, MockConstants.MaximumAllowedCoursePoints, MockConstants.UniqueCourseName);
        }
    }
}