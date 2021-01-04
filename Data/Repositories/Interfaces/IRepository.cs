using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineClinic.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(bool asNoTracking = true);

        Task<IEnumerable<T>> GetAllAsync(bool asNoTracking = true);

        bool Exists(int id);

        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        int Create(T entity);

        Task<int> CreateAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(int id);

        Task DeleteAsync(int id);
    }
}