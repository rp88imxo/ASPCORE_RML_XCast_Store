using PagedList.Core;
using RMLXCast.Web.ViewModels.User;

namespace RMLXCast.Web.ViewModels
{
    public class PagedProductCategoriesViewModel
    {
        public IPagedList<PagedProductCategoryViewModel> PagedProductCategoryViewModels { get; set; } = null!;

        public int PageNumber { get; set; }
    }
}
