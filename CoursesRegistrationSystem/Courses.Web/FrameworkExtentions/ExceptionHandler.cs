namespace Courses.Web.FrameworkExtentions
{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Exceptions;

    //TODO Do not scan the assemblies for custom exceptions every time this is executed!

    /// <summary>
    /// Scans the assembly for all custom exceptions, if the exception is expected (passed as a parameter)
    /// it returns 400 Status Code and the occured Exception's Message as Response Status.
    /// Requires public static string property named CustomMessage on all custom Exceptions.
    /// </summary>
    public class ExceptionHandler : FilterAttribute, IExceptionFilter
    {
        private string[] exceptions;

        public ExceptionHandler(params string[] exceptions)
        {
            this.Exceptions = exceptions;
        }

        public string[] Exceptions
        {
            get { return this.exceptions; }
            set { this.exceptions = value; }
        }

        public void OnException(ExceptionContext filterContext)
        {
            string occuredException = filterContext.Exception.GetType().ToString();

            string occuredExceptionName = GetNameOnly(occuredException, '.');

            var allExceptions = this.GetMainApplicationExceptions();

            Regex regex = new Regex(@"\w*Exception'\w*");

            string exceptionName = String.Empty;

            foreach (var exception in allExceptions)
            {
                Match match = regex.Match(exception.ToString());

                if (match.Success)
                {
                    exceptionName = match.Value.Remove(match.Value.Length - 1);

                    if (exceptionName == occuredExceptionName)
                    {
                        string exceptionMessage = exception.GetType().GetField("CustomMessage").GetValue(null).ToString();

                        filterContext.ExceptionHandled = true;
                        filterContext.HttpContext.Response.StatusCode = 400;
                        filterContext.HttpContext.Response.StatusDescription = exceptionMessage;
                    }
                }
            }
        }

        private string GetNameOnly(string input, char separator)
        {
            int startIndex = input.LastIndexOf(separator);

            return input.Substring(startIndex + 1, (input.Length - 1) - startIndex);
        }

        private IEnumerable<MainApplicationException> GetMainApplicationExceptions()
        {
            return typeof(MainApplicationException)
                        .Assembly.GetTypes()
                        .Where(t => t.IsSubclassOf(typeof(MainApplicationException)) && !t.IsAbstract)
                        .Select(t => (MainApplicationException)Activator.CreateInstance(t));
        }
    }
}