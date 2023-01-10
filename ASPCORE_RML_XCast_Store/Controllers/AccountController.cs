using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RMLXCast.Core.Domain.User;
using RMLXCast.Web.ViewModels.Account;
using RMLXCast.Web.ViewModelsFactories.UserFactory;

namespace RMLXCast.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserViewModelFactory userViewModelFactory;
        private readonly ILogger<AccountController> logger;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserViewModelFactory userViewModelFactory,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.userViewModelFactory = userViewModelFactory;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = userViewModelFactory.CreateApplicationUser(registerViewModel);
                var creationResult = await _userManager.CreateAsync(user, registerViewModel.Password);

                if (creationResult.Succeeded)
                {
                    logger.LogInformation($"Created a new user: {user.Email}:{user.FirstName}:{user.LastName}");
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in creationResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(registerViewModel);
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
            {
                // TODO: Redirect to account panel
                return Ok("You are already logged in!");
            }
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await
                    _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    logger.LogInformation($"User {model.UserName} logged in!");
                    var returnUrl = model.ReturnUrl;
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    logger.LogWarning($"{model.UserName} failed to login on try!");
                    ModelState.AddModelError("", "Неправильный логин/пароль");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied(string? returnUrl = null)
        {
            return Unauthorized("Доступ к данному ресурсу запрещен!");
        }
    }
}
