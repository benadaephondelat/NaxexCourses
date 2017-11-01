namespace Courses.Web.FrameworkExtentions
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using System.Web.Security;

    /// <summary>
    /// If the current user is not authenticated redirect the user
    /// </summary>
    public class CheckIfUserSessionIsExpired : ActionFilterAttribute
    {
        private string controllerName;
        private string actionName;

        public CheckIfUserSessionIsExpired(string controllerName, string actionName)
        {
            this.controllerName = controllerName;
            this.actionName = actionName;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext.Request.IsAuthenticated)
            {
                if (httpContext != null)
                {
                    string sessionCookie = httpContext.Request.Headers["Cookie"];

                    if (string.IsNullOrWhiteSpace(sessionCookie))
                    {
                        FormsAuthentication.SignOut();

                        RouteValueDictionary routeValueDictionary = GetRouteValueDictionary();

                        filterContext.Result = new RedirectToRouteResult(routeValueDictionary);
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// Gets the data concerning where the user should be redirected.
        /// </summary>
        /// <returns>RouteValueDictionary</returns>
        private RouteValueDictionary GetRouteValueDictionary()
        {
            object routeValueData = new { controller = this.controllerName, action = this.actionName };

            RouteValueDictionary routeValueDictionary = new RouteValueDictionary(routeValueData);

            return routeValueDictionary;
        }
    }
}