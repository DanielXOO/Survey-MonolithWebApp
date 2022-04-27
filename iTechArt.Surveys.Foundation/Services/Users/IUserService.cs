using System;
using System.Threading.Tasks;
using iTechArt.Common.Pagination;
using iTechArt.Surveys.DomainModel;
using UserWithSurveysCount = iTechArt.Surveys.Foundation.Models.UserWithSurveysCount;

namespace iTechArt.Surveys.Foundation.Services.Users
{
    public interface IUserService
    {
        Task<PagedResult<UserWithSurveysCount>> GetUsersAsync(int currentPage, int pageSize,
            SortOrder order, string searchRequest);

        Task<ServiceResult> DeleteUsersAsync(User user);

        Task<User> GetUserByIdAsync(Guid id);

        Task<ServiceResult> UpdateAsync(User user);
    }
}