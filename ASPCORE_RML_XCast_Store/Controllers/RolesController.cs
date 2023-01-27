using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMLXCast.Core.Domain.Role;
using RMLXCast.Core.Domain.User;
using RMLXCast.Web.ViewModelsFactories.RolesFactory;
using System.Data;

namespace RMLXCast.Web.Controllers
{
    [Authorize(Roles = "admin,moderator")]
    public class RolesController : Controller
    {
       private readonly RoleManager<ApplicationUserRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRolesViewModelFactory rolesViewModelFactory;

        public RolesController(
            RoleManager<ApplicationUserRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IRolesViewModelFactory rolesViewModelFactory)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.rolesViewModelFactory = rolesViewModelFactory;
        }

        public async Task<IActionResult> Roles()
        {
            var model = await rolesViewModelFactory.GetAllRolesViewModelAsync();
            return View(model);
        }

    }
}
