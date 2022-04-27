using System;
using System.Threading.Tasks;
using iTechArt.Common.Logging;
using iTechArt.Surveys.DomainModel;
using iTechArt.Surveys.Foundation.Services.Account;
using iTechArt.Surveys.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace iTechArt.Surveys.WebApp.Controllers
{
    public sealed class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;


        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _accountService = accountService;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _accountService.SignInAsync(user.Login, user.Password);

            if (!result.IsSuccessful)
            {
                foreach (var error in result.ErrorMessages)
                {
                    ModelState.AddModelError(string.Empty, error);
                }

                return View();
            }
            
            return RedirectToAction(nameof(Index), "Home");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserRegistrationViewModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = new User(){UserName = userModel.Login, DisplayName = userModel.Name};

            var result = await _accountService.RegisterAsync(user, userModel.Password);

            if (!result.IsSuccessful)
            {
                foreach (var error in result.ErrorMessages)
                {
                    ModelState.AddModelError(string.Empty, error);

                    return View();
                }
            }

            await _accountService.SignInAsync(userModel.Login, userModel.Password);

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}