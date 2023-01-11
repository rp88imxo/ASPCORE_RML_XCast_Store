using Microsoft.AspNetCore.Mvc;

namespace RMLXCast.Web.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
