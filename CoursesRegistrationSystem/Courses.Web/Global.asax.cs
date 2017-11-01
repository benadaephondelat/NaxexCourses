namespace Courses.Web
{
    using System.Web.Mvc;
    using System.Reflection;
    using System.Web.Routing;
    using System.Web.Optimization;
    using System.Collections.Generic;

    using FrameworkExtentions.Mappings;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            this.RegisterRazorViewEngineOnly();
            this.ConfigureAutoMapper();
        }

        /// <summary>
        /// Triggers AutoMapper configuration
        /// </summary>
        private void ConfigureAutoMapper()
        {
            List<Assembly> executingAssembly = new List<Assembly> { Assembly.GetExecutingAssembly() };

            AutoMapperConfig autoMapperConfig = new AutoMapperConfig(executingAssembly);

            autoMapperConfig.LoadMappings();
        }

        /// <summary>
        /// Registers RazorViewEngine as the only view engine of the application
        /// </summary>
        private void RegisterRazorViewEngineOnly()
        {
            //TODO WHY NOT JUST Remove the view engine that we do not need?
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
        }
    }
}