using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Courses.Web.Startup))]
namespace Courses.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
