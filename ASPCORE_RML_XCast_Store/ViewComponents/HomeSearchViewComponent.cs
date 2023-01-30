using Microsoft.AspNetCore.Mvc;
using RMLXCast.Web.ViewModels.ShopProduct;

namespace RMLXCast.Web.ViewComponents
{
    public class HomeSearchViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
