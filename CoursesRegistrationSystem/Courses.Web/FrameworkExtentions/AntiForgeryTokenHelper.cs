namespace Courses.Web.FrameworkExtentions
{
    using System.Web.Helpers;

    public static class AntiForgeryTokenHelper
    {
        public static string GetAntiForgeryToken()
        {
            string cookieToken, formToken;

            AntiForgery.GetTokens(null, out cookieToken, out formToken);

            return cookieToken + ":" + formToken;
        }
    }
}