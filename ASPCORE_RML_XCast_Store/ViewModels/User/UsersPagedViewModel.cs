using PagedList.Core;

namespace RMLXCast.Web.ViewModels.User
{
    public class UsersPagedViewModel
    {
        public IPagedList<UserPagedViewModel> UserPagedViewModels { get; set; }

        public int PageNumber { get; set; }
    }
}
