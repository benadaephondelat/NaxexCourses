namespace Courses.ServiceLayer.Tests.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    using DataLayer.Interfaces;
    using Models;
    using Moq;

    /// <summary>
    /// Mocks the data layer
    /// </summary>
    public class DataLayerMockHelper
    {
        private List<ApplicationUser> Users { get; }

        private List<Course> Courses { get; }

        public DataLayerMockHelper()
        {
            this.Users = this.GetDefaultUsersList();
            this.Courses = this.GetDefaultCoursesList();
        }

        public Mock<ICoursesData> SetupTicTacToeDataMock()
        {
            Mock<ICoursesData> dataMock = new Mock<ICoursesData>();

            this.AddApplicationUserRepoMock(dataMock);

            this.AddCoursesRepoMock(dataMock);

            return dataMock;
        }

        /// <summary>
        /// Inserts mocked ApplicationUser repository to the DataLayer mock 
        /// </summary>
        /// <param name="coursesDataMock">DataLayer mock</param>
        private void AddApplicationUserRepoMock(Mock<ICoursesData> coursesDataMock)
        {
            IGenericRepository<ApplicationUser> applicationUserRepoMock = this.SetupApplicationUserRepoMock();

            coursesDataMock.Setup(p => p.Users).Returns(applicationUserRepoMock);
        }

        /// <summary>
        /// Generates a mock of the ApplicationUser repository.
        /// </summary>
        /// <returns>IGenericRepository ApplicationUser</returns>
        private IGenericRepository<ApplicationUser> SetupApplicationUserRepoMock()
        {
            Mock<IGenericRepository<ApplicationUser>> userRepoMock = new Mock<IGenericRepository<ApplicationUser>>();

            userRepoMock.Setup(prop => prop.All()).Returns(this.Users.AsQueryable());

            return userRepoMock.Object;
        }

        private List<ApplicationUser> GetDefaultUsersList()
        {
            List<ApplicationUser> usersList = new List<ApplicationUser>()
            {
                this.userWithMinPoints,
                this.userWithMaxPoints
            };

            return usersList;
        }

        #region Users

        private ApplicationUser userWithMinPoints = new ApplicationUser()
        {
            Id = MockConstants.MinPointsUserId,
            UserName = MockConstants.MinPointsUserUsername,
            CurrentStudentPoints = MockConstants.MinPointsUserCurrentPoints,
            StudentMaxPoints = MockConstants.MinPointsUserMaxPoints
        };

        private ApplicationUser userWithMaxPoints = new ApplicationUser()
        {
            Id = MockConstants.MaxPointsUserId,
            UserName = MockConstants.MaxPointsUserUsername,
            CurrentStudentPoints = MockConstants.MaxPointsUserCurrentPoints,
            StudentMaxPoints = MockConstants.MaxPointsUserMaxPoints
        };

        #endregion

        /// <summary>
        /// Inserts mocked Courses repository to the DataLayer mock 
        /// </summary>
        /// <param name="coursesDataMock">DataLayer mock</param>
        private void AddCoursesRepoMock(Mock<ICoursesData> coursesDataMock)
        {
            IGenericRepository<Course> coursesRepoMock = this.SetupCoursesRepoMock();

            coursesDataMock.Setup(p => p.Courses).Returns(coursesRepoMock);
        }

        /// <summary>
        /// Generates a mock of the Coureses repository.
        /// </summary>
        /// <returns>IGenericRepository Course</returns>
        private IGenericRepository<Course> SetupCoursesRepoMock()
        {
            Mock<IGenericRepository<Course>> courseRepoMock = new Mock<IGenericRepository<Course>>();

            courseRepoMock.Setup(prop => prop.All()).Returns(this.Courses.AsQueryable());

            return courseRepoMock.Object;
        }

        private List<Course> GetDefaultCoursesList()
        {
            List<Course> coursesList = new List<Course>()
            {
                this.emptyCourseWith10Points,
                this.emptyCourseWith5Points
            };

            return coursesList;
        }

        #region Courses

        private Course emptyCourseWith10Points = new Course()
        {
            Id = MockConstants.EmptyCourseId,
            CourseName = MockConstants.EmptyCourseName,
            CourseCreatorId = MockConstants.MinPointsUserId,
            CoursePoints = MockConstants.EmptyCoursePoints
        };

        private Course emptyCourseWith5Points = new Course()
        {
            Id = MockConstants.EmptyCourseWithFivePointsId,
            CourseName = MockConstants.EmptyCourseWithFivePointsName,
            CourseCreatorId = MockConstants.MinPointsUserId,
            CoursePoints = MockConstants.FivePointsCourse
        };

        #endregion
    }
}