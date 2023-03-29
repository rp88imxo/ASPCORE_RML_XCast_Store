using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMLXCast.Services.Catalog;
using RMLXCast.Services.Orders.OrderService;
using RMLXCast.Web.ViewModelsFactories.OrderFactory;
using System.Data;

namespace RMLXCast.Web.Controllers
{
    [Authorize(Roles = "admin,moderator")]
    public class AdminController : Controller
    {
        private readonly IOrderService orderService;
        private readonly IAdminOrderViewModelFactory adminOrderViewModelFactory;

        public AdminController(
            IOrderService orderService,
            IAdminOrderViewModelFactory adminOrderViewModelFactory)
        {
            this.orderService = orderService;
            this.adminOrderViewModelFactory = adminOrderViewModelFactory;
        }


        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Orders(int? page)
        {
            // TODO: Move to the config later...
            var pageValue = page ?? 1;
            int defaultPageSize = 30;

            var orders = await orderService.GetPagedOrdersAsync(pageValue, defaultPageSize, false);
            var totalProductCount = await orderService.GetTotalOrdersCountAsync();

            var model = adminOrderViewModelFactory.CreateOrderPagedViewModel(orders, pageValue, defaultPageSize, totalProductCount);

            return View(model);
        }
    }
}
