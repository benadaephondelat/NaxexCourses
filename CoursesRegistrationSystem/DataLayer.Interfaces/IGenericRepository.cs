namespace DataLayer.Interfaces
{
    using System.Linq;

    public interface IGenericRepository<T>
    {
        IQueryable<T> All();

        T Find(object id);

        void Add(T entity);

        void Update(T entity);

        void UpdateAsync(T entity);

        void Delete(T entity);

        T Delete(object id);
    }
}