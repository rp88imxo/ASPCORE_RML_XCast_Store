using PagedList.Core;

namespace RMLXCast.Web.ViewModels.Product
{
    public class ProductsPagedViewModel
    {
        public IPagedList<ViewProductViewModel> ViewProductViewModels { get; set; }

        public int PageNumber { get; set; }
    }
}
