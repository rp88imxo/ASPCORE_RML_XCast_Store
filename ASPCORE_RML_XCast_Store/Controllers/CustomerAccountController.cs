using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.User;
using RMLXCast.Web.ViewModels.CustomerAccount;
using RMLXCast.Web.ViewModelsFactories.CustomerAccountFactory;
using System.Data;

namespace RMLXCast.Web.Controllers
{
    [Authorize]
    public class CustomerAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICustomerAccountViewModelFactory customerAccountViewModelFactory;
        private readonly ILogger<CustomerAccountController> logger;

        public CustomerAccountController(
            UserManager<ApplicationUser> userManager,
            ICustomerAccountViewModelFactory customerAccountViewModelFactory,
            ILogger<CustomerAccountController> logger)
        {
            this.userManager = userManager;
            this.customerAccountViewModelFactory = customerAccountViewModelFactory;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = await customerAccountViewModelFactory.CreateCustomerAccountViewModelAsync(user);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(CustomerAccountViewModel customerAccountViewModel)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var changePasswordViewModel = customerAccountViewModel.CustomerChangePasswordViewModel;

            var result =
                await userManager.ChangePasswordAsync(user, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);

            if (result.Succeeded)
            {
                logger.LogWarning($"Удачная смена пароля юзером {user.Email}");
            }
            else
            {
                logger.LogError($"Неудачная попытка сменить пароль юзером {user.Email}");
            }

            return RedirectToAction("Index");

        }
    }
}
