using PagedList.Core;

namespace RMLXCast.Web.ViewModels.ShopProduct
{
    public class ShopProductsPagedViewModel
    {
        public IPagedList<ViewShopProductViewModel> viewShopProductViewModels { get; set; } = null!;
        public IList<ShopProductCategoryViewModel> shopProductCategoryViewModels { get; set; } = null!;
        public int PageNumber { get; set; }
        public int? CategoryId { get; set; }
        public string? searchString { get; set; }
    }
}
