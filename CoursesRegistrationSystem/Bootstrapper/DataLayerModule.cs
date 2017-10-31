namespace Bootstrapper
{
    using DataLayer;
    using DataLayer.Data;
    using DataLayer.Interfaces;
    using DataLayer.Repository;

    using Ninject;
    using Ninject.Modules;
    using Ninject.Web.Common;

    public class DataLayerModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>))
                  .WithConstructorArgument("context", Kernel.Get<ApplicationDbContext>());

            Kernel.Bind<ICoursesData>().To<CoursesData>();
            Kernel.Bind<IApplicationDbContext>().To<ApplicationDbContext>().InRequestScope();
        }
    }
}