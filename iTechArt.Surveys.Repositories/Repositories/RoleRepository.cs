using System;
using System.Threading.Tasks;
using iTechArt.Repositories.EFCore;
using iTechArt.Surveys.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Surveys.Repositories.Repositories
{
    public sealed class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) 
            : base(context)
        {
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            var role = await Data
                .FirstOrDefaultAsync(user => user.Name == name);

            return role;
        }

        public async Task<Role> GetByIdAsync(Guid id)
        {
            var role = await Data
                .FirstOrDefaultAsync(role => role.Id == id);

            return role;
        }
    }
}