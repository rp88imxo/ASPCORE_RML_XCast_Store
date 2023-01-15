using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Core.Domain.Role;
using RMLXCast.Core.Domain.User;
using RMLXCast.Services.Catalog.User;
using RMLXCast.Web.ViewModels.User;
using RMLXCast.Web.ViewModelsFactories.UserFactory;
using System.Data;
using System.Linq;

namespace RMLXCast.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IApplicationUserService applicationUserService;
        private readonly IUserViewModelFactory userViewModelFactory;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationUserRole> roleManager;

        public UsersController(
            IApplicationUserService applicationUserService,
            IUserViewModelFactory userViewModelFactory,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationUserRole> roleManager)
        {
            this.applicationUserService = applicationUserService;
            this.userViewModelFactory = userViewModelFactory;
            this.userManager = userManager;
            this.roleManager = roleManager;
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

            var user = await userManager.Users
                .FirstOrDefaultAsync(x=> x.Id == id);
            
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
                var user = await userManager.FindByIdAsync(viewModel.Id);

                if (user == null)
                {
                    return BadRequest("No such id");
                }

                user.FirstName = viewModel.FirstName;
                user.LastName= viewModel.LastName;
                user.IsBanned = viewModel.IsBanned;

                var userRoles = await userManager.GetRolesAsync(user);

                var addedRoles = viewModel.UserRoles.Except(userRoles);
                var removedRoles = userRoles.Except(viewModel.UserRoles);

                await userManager.AddToRolesAsync(user, addedRoles);

                await userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("Users", "Users");
            }

            var roles = await roleManager.Roles.ToListAsync();
            viewModel.AllRoles= roles;

            return View(viewModel);
        }

    }
}
