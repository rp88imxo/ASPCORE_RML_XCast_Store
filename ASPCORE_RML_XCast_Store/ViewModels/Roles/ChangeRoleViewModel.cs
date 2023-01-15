using Microsoft.AspNetCore.Identity;

namespace RMLXCast.Web.ViewModels.Roles
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public IList<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles= new List<IdentityRole>();
            UserRoles= new List<string>();
        }
    }
}
