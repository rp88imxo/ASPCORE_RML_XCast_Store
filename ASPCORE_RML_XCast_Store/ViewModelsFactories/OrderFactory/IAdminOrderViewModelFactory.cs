using RMLXCast.Core.Domain.Orders;
using RMLXCast.Web.ViewModels.Orders;

namespace RMLXCast.Web.ViewModelsFactories.OrderFactory
{
    public interface IAdminOrderViewModelFactory
    {
        AdminOrdersPagedViewModel CreateOrderPagedViewModel(ICollection<Order> orders, int pageNumber, int pageSize, int totalOrders);
    }
}