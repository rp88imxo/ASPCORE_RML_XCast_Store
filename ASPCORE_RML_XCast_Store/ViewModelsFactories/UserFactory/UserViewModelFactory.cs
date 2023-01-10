using RMLXCast.Core.Domain.User;
using RMLXCast.Web.ViewModels.Account;

namespace RMLXCast.Web.ViewModelsFactories.UserFactory
{
    public class UserViewModelFactory : IUserViewModelFactory
    {
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
    }
}
