using PagedList.Core;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Core.Domain.Orders;
using RMLXCast.Services.Orders.Order_Status_Formatters;
using RMLXCast.Services.Price.Price_Formatters;
using RMLXCast.Web.ViewModels.Orders;
using RMLXCast.Web.ViewModels.Product;

namespace RMLXCast.Web.ViewModelsFactories.OrderFactory
{
    public class AdminOrderViewModelFactory : IAdminOrderViewModelFactory
    {
        private readonly IPriceFormatter priceFormatter;
        private readonly IOrderStatusFormatter orderStatusFormatter;

        public AdminOrderViewModelFactory(
            IPriceFormatter priceFormatter,
            IOrderStatusFormatter orderStatusFormatter
            )
        {
            this.priceFormatter = priceFormatter;
            this.orderStatusFormatter = orderStatusFormatter;
        }


        public AdminOrdersPagedViewModel CreateOrderPagedViewModel(ICollection<Order> orders, int pageNumber, int pageSize, int totalOrders)
        {
            var model = new AdminOrdersPagedViewModel();

            model.PageNumber = pageNumber;
            model.ViewOrderViewModels = new StaticPagedList<ViewAdminOrderViewModel>(orders
                .Select(x => new ViewAdminOrderViewModel
                {
                    Id = x.Id,
                    CreatedOnUtcFormatted = x.CreatedOnUtc.ToShortDateString(),
                    TotalPriceFormatted = priceFormatter.GetFormattedPrice(x.TotalPrice),
                    OrderStatusFormatted = orderStatusFormatter.GetFormattedOrderStatus(x.OrderStatus),
                    CustomerFirstName = x.ApplicationUser.FirstName,
                    CustomerLastName = x.ApplicationUser.LastName,
                })
                .ToList()
                , pageNumber, pageSize, totalOrders);

            return model;
        }
    }
}
