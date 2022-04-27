using System;
using System.Threading.Tasks;
using iTechArt.Repositories;
using iTechArt.Surveys.DomainModel;

namespace iTechArt.Surveys.Repositories.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetByIdAsync(Guid id);
        Task<Role> GetByNameAsync(string name);
    }
}