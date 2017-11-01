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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                        .HasMany<Course>(s => s.Courses)
                        .WithMany(c => c.ApplicationUsers)
                        .Map(cs =>
                        {
                            cs.MapLeftKey("ApplicationUserRefId");
                            cs.MapRightKey("CourseRefId");
                            cs.ToTable("CoursesWithStudents");
                        });

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}