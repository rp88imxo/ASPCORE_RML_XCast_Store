using Microsoft.AspNetCore.Mvc;

namespace RMLXCast.Web.ViewComponents
{
    public class MobileHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
