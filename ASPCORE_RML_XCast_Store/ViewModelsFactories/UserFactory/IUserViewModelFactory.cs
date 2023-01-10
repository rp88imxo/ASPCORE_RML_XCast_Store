using RMLXCast.Core.Domain.User;
using RMLXCast.Web.ViewModels.Account;

namespace RMLXCast.Web.ViewModelsFactories.UserFactory
{
    public interface IUserViewModelFactory
    {
        ApplicationUser CreateApplicationUser(RegisterViewModel registerViewModel);
    }
}