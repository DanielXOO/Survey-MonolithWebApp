using System;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Common.Pagination;
using iTechArt.Repositories.EFCore;
using iTechArt.Surveys.DomainModel;
using iTechArt.Surveys.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace iTechArt.Surveys.Repositories.Repositories
{
    public sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        
        public async Task<PagedResult<UserWithSurveysCount>> GetUsersAsync(int pageSize, int currentPage,
            string searchRequest, SortOrder sortOrder)
        {
            var users = GetUsersQuery();

            if (!string.IsNullOrEmpty(searchRequest))
            {
                users = users.Where(user => user.DisplayName.Contains(searchRequest));
            }

            switch (sortOrder)
            {
                case SortOrder.Descending:
                    users = users.OrderByDescending(user => user.DisplayName);
                    break;
                case SortOrder.Ascending:
                    users = users.OrderBy(user => user.DisplayName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortOrder), sortOrder, "Unknown sort order value");
            }

            var userWithSurveysCount = users.Select(user =>
                new UserWithSurveysCount()
                {
                    User = user,
                    SurveysCount = user.Surveys.Count
                });

            var result = await userWithSurveysCount.ToPagedResultAsync(pageSize, currentPage);

            return result;
        }

        public async Task<User> GetByNameAsync(string name)
        {
            var user = await GetUsersQuery()
                .FirstOrDefaultAsync(user => user.UserName == name);

            return user;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var user = await GetUsersQuery()
                .FirstOrDefaultAsync(user => user.Id == id);

            return user;
        }


        private IQueryable<User> GetUsersQuery()
        {
            return Data
                .Include(user => user.Roles)
                .Include(user => user.Surveys);
        }
    }
}