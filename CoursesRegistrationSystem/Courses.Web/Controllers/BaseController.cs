namespace Courses.Web.Controllers
{
    using System;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    public abstract class BaseController : Controller
    {
        public Func<string> CurrentUserName;

        public BaseController()
        {
            this.CurrentUserName = this.GetUserIdentityUsername;
        }

        /// <summary>
        /// Returns the current user's username
        /// </summary>
        /// <returns>string</returns>
        private string GetUserIdentityUsername()
        {
            string result = this.User.Identity.GetUserName();

            return result;
        }
    }
}