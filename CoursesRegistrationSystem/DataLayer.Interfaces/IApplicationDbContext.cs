namespace DataLayer.Interfaces
{
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Models;

    public interface IApplicationDbContext
    {
        IDbSet<Course> Courses { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}