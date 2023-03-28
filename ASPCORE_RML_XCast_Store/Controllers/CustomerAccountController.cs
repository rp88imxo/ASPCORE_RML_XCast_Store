using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.User;
using RMLXCast.Web.ViewModelsFactories.CustomerAccountFactory;
using System.Data;

namespace RMLXCast.Web.Controllers
{
    [Authorize]
    public class CustomerAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICustomerAccountViewModelFactory customerAccountViewModelFactory;

        public CustomerAccountController(
            UserManager<ApplicationUser> userManager,
            ICustomerAccountViewModelFactory customerAccountViewModelFactory)
        {
            this.userManager = userManager;
            this.customerAccountViewModelFactory = customerAccountViewModelFactory;
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
    }
}
