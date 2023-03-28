using RMLXCast.Core.Domain.User;
using RMLXCast.Services.Orders.Order_Status_Formatters;
using RMLXCast.Services.Orders.OrderService;
using RMLXCast.Services.Price.Price_Formatters;
using RMLXCast.Web.ViewModels.CustomerAccount;

namespace RMLXCast.Web.ViewModelsFactories.CustomerAccountFactory
{
    public class CustomerAccountViewModelFactory : ICustomerAccountViewModelFactory
    {
        private readonly IOrderService orderService;
        private readonly IPriceFormatter priceFormatter;
        private readonly IOrderStatusFormatter orderStatusFormatter;

        public CustomerAccountViewModelFactory(
            IOrderService orderService,
            IPriceFormatter priceFormatter,
            IOrderStatusFormatter orderStatusFormatter)
        {
            this.orderService = orderService;
            this.priceFormatter = priceFormatter;
            this.orderStatusFormatter = orderStatusFormatter;
        }

        public async Task<CustomerAccountViewModel> CreateCustomerAccountViewModelAsync(ApplicationUser applicationUser)
        {
            var model = new CustomerAccountViewModel()
            {
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                CustomerOrdersViewModel = new CustomerOrdersViewModel()
                {
                    customerOrderViewModels = await GetUserOrdersViewModels(applicationUser)
                }
            };

            return model;
        }

        private async Task<IList<CustomerOrderViewModel>> GetUserOrdersViewModels(ApplicationUser applicationUser)
        {
            var orders = await orderService.GetAllOrdersForUserAsync(applicationUser);
            return orders
                  .Select(x => new CustomerOrderViewModel()
                  {
                      Id = x.Id,
                      CreatedOnFormatted = x.CreatedOnUtc.ToShortDateString(),
                      TotalPriceFormatted = priceFormatter.GetFormattedPrice(x.TotalPrice),
                      OrderStatusFormatted = orderStatusFormatter.GetFormattedOrderStatus(x.OrderStatus)
                  })
                  .ToList();
        }
    }
}
