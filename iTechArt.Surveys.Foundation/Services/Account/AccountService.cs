using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iTechArt.Common.Time;
using iTechArt.Surveys.DomainModel;
using Microsoft.AspNetCore.Identity;

namespace iTechArt.Surveys.Foundation.Services.Account
{
    public sealed class AccountService : IAccountService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ISystemClock _systemClock;


        public AccountService(SignInManager<User> signInManager,
            UserManager<User> userManager, ISystemClock systemClock)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _systemClock = systemClock;
        }


        public async Task<ServiceResult> SignInAsync(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, true, false);

            return ConvertToServiceResult(result);
        }
        
        public async Task<ServiceResult> RegisterAsync(User user, string password)
        {
            user.Roles ??= new List<Role>();
            user.CreationTime = _systemClock.UtcNow;
            var result = await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, RoleNames.User);

            return ConvertToServiceResult(result);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
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

        private static ServiceResult ConvertToServiceResult(SignInResult result)
        {
            if (!result.Succeeded)
            {
                return ServiceResult.CreateFailed(new[]
                {
                    "Login or Password is not correct"
                });
            }

            return ServiceResult.CreateSuccessful();
        }
    }
}