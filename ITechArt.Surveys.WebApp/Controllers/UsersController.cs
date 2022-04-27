using System.Threading.Tasks;
using iTechArt.Common.Pagination;
using iTechArt.Surveys.DomainModel;
using iTechArt.Surveys.Foundation.Models;
using iTechArt.Surveys.Foundation.Services.Account;
using iTechArt.Surveys.Foundation.Services.Users;
using iTechArt.Surveys.WebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Surveys.WebApp.Controllers
{
    [Authorize(Roles = RoleNames.Admin)]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;


        public UsersController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
        }


        public async Task<IActionResult> Index(string nameSearchTerm = "", SortOrder sortOrder = SortOrder.Ascending,
            int page = 1)
        {
            const int pageSize = 5;

            var users = await _userService.GetUsersAsync(page, pageSize, sortOrder, nameSearchTerm);

            var pageResponse = new PageResponseViewModel<UserWithSurveysCount>()
            {
                Items = users,
                NameSearchTerm = nameSearchTerm,
                SortOrder = sortOrder
            };

            if (users.TotalPages < users.CurrentPage && users.TotalPages > 0)
            {
                return RedirectToAction(nameof(Index),
                    new
                    {
                        searchRequest = pageResponse.NameSearchTerm,
                        sortOrder = pageResponse.SortOrder, 
                        page = users.TotalPages
                    });
            }

            return View(pageResponse);
        }

        public IActionResult DeleteUser(UserDeleteViewModel user)
        {
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(UserDeleteViewModel userDelete)
        {
            var user = await _userService.GetUserByIdAsync(userDelete.Id);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, $"User doesn't exist");

                return View(nameof(DeleteUser), userDelete);
            }

            if (user.UserName == HttpContext.User.Identity?.Name)
            {
                ModelState.AddModelError(string.Empty, $"You can't delete yourself");

                return View(nameof(DeleteUser), userDelete);
            }

            var result = await _userService.DeleteUsersAsync(user);

            if (!result.IsSuccessful)
            {
                foreach (var error in result.ErrorMessages)
                {
                    ModelState.AddModelError(string.Empty, error);

                    return View(nameof(DeleteUser), userDelete);
                }
            }

            return Redirect(userDelete.ReturnUrl);
        }

        public IActionResult EditUser(UserEditViewModel user)
        {
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditConfirm(UserEditViewModel userEdit)
        {
            var user = await _userService.GetUserByIdAsync(userEdit.Id);
            
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, $"Unable to edit user");

                return View(nameof(EditUser), userEdit);
            }

            user.DisplayName = userEdit.Name;
            var result = await _userService.UpdateAsync(user);

            if (!result.IsSuccessful)
            {
                foreach (var error in result.ErrorMessages)
                {
                    ModelState.AddModelError(string.Empty, error);

                    return View(nameof(EditUser), userEdit);
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}