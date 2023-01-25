using Microsoft.AspNetCore.Mvc;
using RMLXCast.Web.ViewModels;
using System.Diagnostics;

namespace RMLXCast.Web.Controllers
{
    public class ShopController : Controller
    {



        public IActionResult Products()
        {
            return View();
        }

        public IActionResult ErrorNotFound()
        {
            return View();
        }
    }
}
