using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Core.Domain.Role;
using RMLXCast.Core.Domain.User;
using RMLXCast.Web.ViewModels.Account;
using RMLXCast.Web.ViewModels.User;
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

        public Task<UsersPagedViewModel> CreateUsersPagedViewModelAsync(ICollection<ApplicationUser> users,
            int pageNumber,
            int pageSize,
            int totalUsers)
        {

            var usersModel = users.Select(x => new UserPagedViewModel()
            {
                UserId = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                RegistrationDateUtc = x.RegistrationDateUtc,
                Roles = CreateUserRolesString(x.ApplicationUserRoles),
                OrdersCount = x.Orders.Count()
            }).ToList();

            var model = new UsersPagedViewModel()
            {
                UserPagedViewModels = new StaticPagedList<UserPagedViewModel>(usersModel,
                    pageNumber,
                    pageSize,
                    totalUsers),

                PageNumber = pageNumber,
            };

            return Task.FromResult(model);
        }

        public async Task<EditUserViewModel> GetEditUserViewModelAsync(ApplicationUser user)
        {
            var roles = await roleManager.Roles.ToListAsync();


            var model = new EditUserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AllRoles = roles,
                UserRoles = user.ApplicationUserRoles.Select(x => x.Name).ToList()!,
                IsBanned = user.IsBanned,
            };

            return model;
        }

        private string CreateUserRolesString(IList<ApplicationUserRole> roles)
        {
            var sb = new StringBuilder();

            foreach (var role in roles)
            {
                sb.Append(role.Name);
                sb.Append(",");
            }

            return sb.ToString();
        }
    }
}
