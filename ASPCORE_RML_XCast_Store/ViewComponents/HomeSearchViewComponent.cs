using Microsoft.AspNetCore.Mvc;

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
