using System.Collections.Generic;
using System.Threading.Tasks;

namespace iTechArt.Repositories
{
    public interface IRepository<T>
    {
        void Create(T item);

        Task<T> FindByIdAsync(params object[] keyValues);

        Task<IReadOnlyCollection<T>> GetAllAsync();

        void Update(T item);

        void Delete(T item);
    }
}