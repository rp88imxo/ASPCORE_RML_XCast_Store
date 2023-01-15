using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.User;
using RMLXCast.Services.Catalog.User;
using RMLXCast.Web.ViewModels.User;
using RMLXCast.Web.ViewModelsFactories.UserFactory;

namespace RMLXCast.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly IUserViewModelFactory userViewModelFactory;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(
            IApplicationUserService applicationUserService,
            IUserViewModelFactory userViewModelFactory,
            UserManager<ApplicationUser> userManager
            )
        {
            this.applicationUserService = applicationUserService;
            this.userViewModelFactory = userViewModelFactory;
            this.userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Users(int? page)
        {
            // TODO: Move to the config later...
            var pageValue = page ?? 1;
            int defaultPageSize = 30;

            var users = await applicationUserService.GetPagedUsersAsync(pageValue, defaultPageSize);
            var totalUsers = await applicationUserService.GetTotalUsersAsync();
            var model = await userViewModelFactory.CreateUsersPagedViewModelAsync(users, pageValue, defaultPageSize, totalUsers);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("[EditUser]: Model is not valid!");
            }

            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return BadRequest();
            }

            var model = await userViewModelFactory.GetEditUserViewModelAsync(user);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {

            }

            return View(viewModel);
        }

    }
}
