using System;
using System.Threading.Tasks;
using iTechArt.Repositories;
using iTechArt.Surveys.DomainModel;

namespace iTechArt.Surveys.Repositories.Repositories
{
    public interface IFileRepository : IRepository<FileInfo>
    {
        Task<FileInfo> GetByIdAsync(Guid id);
    }
}