using System;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Common.Extensions;
using iTechArt.Common.Pagination;
using iTechArt.Surveys.DomainModel;
using iTechArt.Surveys.Foundation.Models;
using iTechArt.Surveys.Repositories;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.Surveys.Foundation.Services.Users
{
    public sealed class UserService : IUserService
    {
        private readonly ISurveysUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;


        public UserService(ISurveysUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            return user;
        }

        public async Task<PagedResult<UserWithSurveysCount>> GetUsersAsync(int currentPage, int pageSize,
            SortOrder order, string searchRequest)
        {
            var usersPaged = await _unitOfWork.Users
                .GetUsersAsync(pageSize, currentPage, searchRequest, order);

            var usersPagedWithSurveysCount = usersPaged.MapPagedResult(user => new UserWithSurveysCount()
            {
                User = user.User,
                SurveysCount = user.SurveysCount
            });

            return usersPagedWithSurveysCount;
        }

        public async Task<ServiceResult> DeleteUsersAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);

            return ConvertToServiceResult(result);
        }

        public async Task<ServiceResult> UpdateAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);

            return ConvertToServiceResult(result);
        }
        

        private static ServiceResult ConvertToServiceResult(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToArray();

                return ServiceResult.CreateFailed(errors);
            }

            return ServiceResult.CreateSuccessful();
        }
    }
}