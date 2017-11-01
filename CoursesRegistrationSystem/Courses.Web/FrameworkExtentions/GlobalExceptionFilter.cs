namespace Courses.Web.FrameworkExtentions
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Handles all unhandled exceptions, redirect the user to Home/Error
    /// </summary>
    public class GlobalExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled == false)
            {
                filterContext.ExceptionHandled = true;

                object routeValueData = new { controller = "Home", action = "Error" };

                RouteValueDictionary routeValueDictionary = new RouteValueDictionary(routeValueData);

                filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
            }
        }
    }
}