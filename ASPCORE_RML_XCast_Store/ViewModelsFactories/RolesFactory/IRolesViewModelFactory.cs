using RMLXCast.Web.ViewModels.Roles;

namespace RMLXCast.Web.ViewModelsFactories.RolesFactory
{
    public interface IRolesViewModelFactory
    {
        Task<AllRolesViewModel> GetAllRolesViewModelAsync();
    }
}