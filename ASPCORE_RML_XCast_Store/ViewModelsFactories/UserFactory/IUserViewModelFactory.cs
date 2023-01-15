using RMLXCast.Core.Domain.User;
using RMLXCast.Web.ViewModels.Account;
using RMLXCast.Web.ViewModels.User;

namespace RMLXCast.Web.ViewModelsFactories.UserFactory
{
    public interface IUserViewModelFactory
    {
        ApplicationUser CreateApplicationUser(RegisterViewModel registerViewModel);
        Task<UsersPagedViewModel> CreateUsersPagedViewModelAsync(ICollection<ApplicationUser> users, int pageNumber, int pageSize, int totalUsers);
        Task<EditUserViewModel> GetEditUserViewModelAsync(ApplicationUser user);
    }
}