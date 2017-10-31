namespace DataLayer
{
    using System.Data.Entity;

    using Interfaces;
    using Models;

    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public IDbSet<Course> Courses { get; set; }

        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}