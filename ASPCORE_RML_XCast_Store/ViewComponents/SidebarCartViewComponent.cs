using Microsoft.AspNetCore.Mvc;

namespace RMLXCast.Web.ViewComponents
{
    public class SidebarCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
