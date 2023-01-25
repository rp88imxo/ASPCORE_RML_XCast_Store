using Microsoft.AspNetCore.Mvc;

namespace RMLXCast.Web.ViewComponents
{
    public class ProductCategoriesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("ProductCategories");
        }
    }
}
