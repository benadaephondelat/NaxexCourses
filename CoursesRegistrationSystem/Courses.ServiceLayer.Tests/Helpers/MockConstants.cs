namespace Courses.ServiceLayer.Tests.Helpers
{
    public static class MockConstants
    {
        public static string InvalidUserId = "no-such-id-in-the-database";

        public static string InvalidUserUsername = "no-such-username-in-the-database";

        public static int InvalidCourseId = 999;


        public static string MinPointsUserId = "min-points-user-id";

        public static string MinPointsUserUsername = "georgi_iliev@yahoo.com";

        public static double MinPointsUserCurrentPoints = 0D;

        public static double MinPointsUserMaxPoints = 100D;


        public static string MaxPointsUserId = "max-points-user-id";

        public static string MaxPointsUserUsername = "new@yahoo.com";

        public static double MaxPointsUserCurrentPoints = 100D;

        public static double MaxPointsUserMaxPoints = 100D;


        public static int EmptyCourseId = 1;

        public static string EmptyCourseName = "EmptyCourse";

        public static double EmptyCoursePoints = 10D;


        public static int EmptyCourseWithFivePointsId = 2;

        public static string EmptyCourseWithFivePointsName = "EmptyCourseWithFivePoints";

        public static double FivePointsCourse = 5D;


        public static double MaximumAllowedCoursePoints = 10D;

        public static string UniqueCourseName = "UniqueCourseName";
    }
}