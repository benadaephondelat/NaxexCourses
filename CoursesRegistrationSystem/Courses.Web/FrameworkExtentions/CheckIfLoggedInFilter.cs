namespace Courses.Web.FrameworkExtentions
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// If the current user is not authenticated redirect the user to the Home page
    /// </summary>
    public class CheckIfLoggedInFilter : ActionFilterAttribute
    {
        private const string ControllerName = "Home";
        private const string ActionMethodName = "Index";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentUser = filterContext.RequestContext.HttpContext.User;

            if (UserIdentityHelper.UserIsLoggedIn(currentUser) == false)
            {
                RouteValueDictionary routeValueDictionary = GetRouteValueDictionary();

                filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Gets the data concerning where the user should be redirected.
        /// </summary>
        /// <returns>RouteValueDictionary</returns>
        private static RouteValueDictionary GetRouteValueDictionary()
        {
            object routeValueData = new { controller = ControllerName, action = ActionMethodName };

            RouteValueDictionary routeValueDictionary = new RouteValueDictionary(routeValueData);

            return routeValueDictionary;
        }
    }
}