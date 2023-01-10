using Microsoft.AspNetCore.Mvc;

namespace RMLXCast.Web.ViewComponents
{
    public class HeaderAsideViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
