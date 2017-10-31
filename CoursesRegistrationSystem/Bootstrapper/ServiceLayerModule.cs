namespace Bootstrapper
{
    using ServiceLayer;
    using ServiceLayer.Interfaces;

    using Ninject.Modules;

    public class ServiceLayerModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ICoursesService>().To<CoursesService>();
        }
    }
}