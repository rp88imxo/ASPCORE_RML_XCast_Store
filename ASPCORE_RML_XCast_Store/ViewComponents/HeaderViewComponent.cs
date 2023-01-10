using Microsoft.AspNetCore.Mvc;

namespace RMLXCast.Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
