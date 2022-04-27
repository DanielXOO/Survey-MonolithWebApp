using System;
using System.Threading.Tasks;
using iTechArt.Repositories.EFCore;
using iTechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Surveys.Repositories.Repositories
{
    public class FileRepository : Repository<FileInfo>, IFileRepository
    {
        public FileRepository(DbContext context) : base(context)
        {
        }

        public async Task<FileInfo> GetByIdAsync(Guid id)
        {
            var file = await Data.FirstOrDefaultAsync(file => file.Id == id);

            return file;
        }
    }
}