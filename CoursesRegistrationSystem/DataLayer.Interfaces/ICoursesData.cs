namespace DataLayer.Interfaces
{
    using Models;
    using System.Threading.Tasks;

    public interface ICoursesData
    {
        IGenericRepository<Course> Courses { get; }

        IGenericRepository<ApplicationUser> Users { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}