using System.Web;
using System.Web.Optimization;

namespace Courses.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                   .Include("~/Scripts/jquery-2.1.4.js")
                   .Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/mvc-grid-css")
                   .Include("~/Content/MvcGrid/mvc-grid.css"));

            bundles.Add(new ScriptBundle("~/bundles/mvc-grid")
                   .Include("~/Scripts/jquery-2.1.4.js")
                   .Include("~/Scripts/MvcGrid/mvc-grid.js"));

            bundles.Add(new ScriptBundle("~/bundles/manageCoursesGridModule")
                   .Include("~/Scripts/Custom/manageCoursesGridModule.js"));

            bundles.Add(new ScriptBundle("~/bundles/availableCoursesModule")
                   .Include("~/Scripts/Custom/availableCoursesModule.js"));

            #if DEBUG
                BundleTable.EnableOptimizations = false;
            #else
                BundleTable.EnableOptimizations = true;
            #endif
        }
    }
}
