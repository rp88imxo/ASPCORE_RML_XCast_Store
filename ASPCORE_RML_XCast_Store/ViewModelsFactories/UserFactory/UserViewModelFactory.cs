using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Core.Domain.Role;
using RMLXCast.Core.Domain.User;
using RMLXCast.Web.ViewModels.Account;
using RMLXCast.Web.ViewModels.User;
using System.Data;
using System.Text;

namespace RMLXCast.Web.ViewModelsFactories.UserFactory
{
    public class UserViewModelFactory : IUserViewModelFactory
    {
        private readonly RoleManager<ApplicationUserRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserViewModelFactory(
            RoleManager<ApplicationUserRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        public ApplicationUser CreateApplicationUser(RegisterViewModel registerViewModel)
        {
            var user = new ApplicationUser()
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                RegistrationDateUtc = DateTime.UtcNow
            };

            return user;
        }

        public async Task<UsersPagedViewModel> CreateUsersPagedViewModelAsync(ICollection<ApplicationUser> users,
            int pageNumber,
            int pageSize,
            int totalUsers)
        {
            var models = new List<UserPagedViewModel>();
            foreach (var u in users)
            {
                var user = new UserPagedViewModel()
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    RegistrationDateUtc = u.RegistrationDateUtc,

                    OrdersCount = u.Orders.Count()
                };

                var roles = await userManager.GetRolesAsync(u);
                user.Roles = CreateUserRolesString(roles);

                models.Add(user);
            }

            var model = new UsersPagedViewModel()
            {
                UserPagedViewModels = new StaticPagedList<UserPagedViewModel>(models.ToList(),
                    pageNumber,
                    pageSize,
                    totalUsers),

                PageNumber = pageNumber,
            };

            return model;
        }

        public async Task<EditUserViewModel> GetEditUserViewModelAsync(ApplicationUser user)
        {
            var roles = await roleManager.Roles.ToListAsync();
            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AllRoles = roles,
                UserRoles = userRoles,
                IsBanned = user.IsBanned,
            };

            return model;
        }

        private string CreateUserRolesString(IList<string> roles)
        {
            var sb = new StringBuilder();

            sb.AppendJoin(",", roles);

            return sb.ToString();
        }
    }
}
