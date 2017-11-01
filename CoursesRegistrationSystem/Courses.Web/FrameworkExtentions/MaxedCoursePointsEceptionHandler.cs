namespace Courses.Web.FrameworkExtentions
{
    using System.Web.Mvc;

    //TODO We can scan use something like the ExceptionHandler to avoid repeating logic

    /// <summary>
    /// Handles MaxedCoursePointsEceptionHandler
    /// Returns 400 Status Code and the occured Exception's Message as Response Status.
    /// </summary>
    public class MaxedCoursePointsEceptionHandler : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string occuredException = filterContext.Exception.GetType().ToString();

            string occuredExceptionName = this.GetNameOnly(occuredException, '.');

            if (occuredExceptionName == "MaxedCoursePointsException")
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.StatusCode = 400;
                filterContext.HttpContext.Response.StatusDescription = "You do not have sufficient points to enroll in that course";

                filterContext.Result.ExecuteResult(filterContext.Controller.ControllerContext);
            }
        }

        private string GetNameOnly(string input, char separator)
        {
            int startIndex = input.LastIndexOf(separator);

            return input.Substring(startIndex + 1, (input.Length - 1) - startIndex);
        }
    }
}