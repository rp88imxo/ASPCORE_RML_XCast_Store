using Microsoft.AspNetCore.Mvc;

namespace RMLXCast.Web.Controllers
{
    public class AdminProductsController : Controller
    {
        public IActionResult Products()
        {
            return View();
        }
    }
}
