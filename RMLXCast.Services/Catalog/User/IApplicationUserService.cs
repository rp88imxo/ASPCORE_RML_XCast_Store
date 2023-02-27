using RMLXCast.Core.Domain.User;

namespace RMLXCast.Services.Catalog.User
{
    public interface IApplicationUserService
    {
        Task<IList<ApplicationUser>> GetPagedUsersAsync(int pageNumber, int pageSize);
        Task<int> GetTotalUsersAsync();
        Task<ApplicationUser> GetUserWithAddress(ApplicationUser applicationUser);
    }
}