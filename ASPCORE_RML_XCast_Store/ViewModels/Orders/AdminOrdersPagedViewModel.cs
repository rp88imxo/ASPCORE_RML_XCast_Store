using PagedList.Core;
using RMLXCast.Web.ViewModels.Product;

namespace RMLXCast.Web.ViewModels.Orders
{
    public class AdminOrdersPagedViewModel
    {
        public IPagedList<ViewAdminOrderViewModel> ViewOrderViewModels { get; set; }

        public int PageNumber { get; set; }
    }
}
