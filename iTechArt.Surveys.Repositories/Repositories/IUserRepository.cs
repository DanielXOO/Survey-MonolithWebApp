using System;
using System.Threading.Tasks;
using iTechArt.Common.Pagination;
using iTechArt.Repositories;
using iTechArt.Surveys.DomainModel;
using iTechArt.Surveys.Repositories.Models;

namespace iTechArt.Surveys.Repositories.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<PagedResult<UserWithSurveysCount>> GetUsersAsync(int pageSize, int currentPage,
            string searchRequest, SortOrder sortOrder);

        Task<User> GetByNameAsync(string name);

        Task<User> GetByIdAsync(Guid id);
    }
}