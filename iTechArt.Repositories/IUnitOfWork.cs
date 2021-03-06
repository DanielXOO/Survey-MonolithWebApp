using System.Threading.Tasks;

namespace iTechArt.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<T> GetRepository<T>() where T : class;

        Task SaveChangesAsync();
    }
}