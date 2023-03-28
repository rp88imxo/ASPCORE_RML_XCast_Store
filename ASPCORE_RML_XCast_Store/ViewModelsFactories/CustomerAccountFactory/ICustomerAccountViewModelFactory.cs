using RMLXCast.Core.Domain.User;
using RMLXCast.Web.ViewModels.CustomerAccount;

namespace RMLXCast.Web.ViewModelsFactories.CustomerAccountFactory
{
    public interface ICustomerAccountViewModelFactory
    {
        Task<CustomerAccountViewModel> CreateCustomerAccountViewModelAsync(ApplicationUser applicationUser);
    }
}