using RMLXCast.Core.Domain.Orders;

namespace RMLXCast.Services.Orders.Order_Status_Formatters
{
    public interface IOrderStatusFormatter
    {
        string GetFormattedOrderStatus(OrderStatus orderStatus);
    }
}