using Microsoft.AspNetCore.Mvc;
using RMLXCast.Web.ViewModels.ShopProduct;

namespace RMLXCast.Web.ViewComponents
{
    public class ProductCategoriesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IList<ShopProductCategoryViewModel> shopProductCategoryViewModels)
        {
            return View("ProductCategories", shopProductCategoryViewModels);
        }
    }
}
