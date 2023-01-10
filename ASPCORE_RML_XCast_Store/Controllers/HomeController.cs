using Microsoft.AspNetCore.Mvc;
using RMLXCast.Services.Catalog;
using RMLXCast.Web.ViewModels;
using System.Diagnostics;

namespace RMLXCast.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductService productService)
        {
            _logger = logger;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}