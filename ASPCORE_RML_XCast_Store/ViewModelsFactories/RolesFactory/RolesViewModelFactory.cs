using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMLXCast.Core.Domain.Role;
using RMLXCast.Web.ViewModels.Roles;

namespace RMLXCast.Web.ViewModelsFactories.RolesFactory
{
    public class RolesViewModelFactory : IRolesViewModelFactory
    {
        private readonly RoleManager<ApplicationUserRole> _roleManager;

        public RolesViewModelFactory(RoleManager<ApplicationUserRole> roleManager)
        {
            this._roleManager = roleManager;
        }

        public async Task<AllRolesViewModel> GetAllRolesViewModelAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var model = new AllRolesViewModel
            {
                AllRoles = roles
            };

            return model;
        }
    }
}
