using RMLXCast.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Services.Orders.Order_Status_Formatters
{
    public class OrderStatusFormatter : IOrderStatusFormatter
    {
        public string GetFormattedOrderStatus(OrderStatus orderStatus)
        {
            return orderStatus switch
            {
                OrderStatus.Pending => "Ожидает оплаты",
                OrderStatus.Processing => "Упаковка",
                OrderStatus.Sent => "Отправлен",
                OrderStatus.Complete => "Завершен",
                OrderStatus.Cancelled => "Отменен",
                _ => "Неверный код статуса",
            };
        }
    }
}
