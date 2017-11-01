namespace Courses.Web.FrameworkExtentions
{
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;

    /// <summary>
    /// Validates AntiForgeryToken for custom Ajax Requests.
    /// </summary>
    public class ValidateAntiForgeryTokenAjax : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    ValidateRequestHeader(filterContext.HttpContext.Request);
                }
                else
                {
                    AntiForgery.Validate();
                }
            }
            catch (HttpAntiForgeryException)
            {
                throw new HttpAntiForgeryException("Anti forgery token cookie not found");
            }
        }

        private void ValidateRequestHeader(HttpRequestBase request)
        {
            string cookieToken = string.Empty;
            string formToken = string.Empty;

            string tokenValue = request.Headers["RequestVerificationToken"];

            if (string.IsNullOrEmpty(tokenValue) == false)
            {
                string[] tokens = tokenValue.Split(':');

                if (tokens.Length == 2)
                {
                    cookieToken = tokens[0].Trim();
                    formToken = tokens[1].Trim();
                }
            }

            AntiForgery.Validate(cookieToken, formToken);
        }
    }
}