using Microsoft.AspNetCore.Identity;
using RMLXCast.Core.Domain.Role;

namespace RMLXCast.Web.ViewModels.Roles
{
    public class AllRolesViewModel
    {
        public IList<ApplicationUserRole> AllRoles { get; set; } = null!;
    }
}
