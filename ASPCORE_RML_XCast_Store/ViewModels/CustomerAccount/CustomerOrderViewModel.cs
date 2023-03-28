using RMLXCast.Core.Domain.Orders;

namespace RMLXCast.Web.ViewModels.CustomerAccount
{
    public class CustomerOrderViewModel
    {
        public int Id { get; set; }

        public string OrderStatusFormatted { get; set; } // formatted as string

        public string CreatedOnFormatted { get; set; }

        public string TotalPriceFormatted { get; set; }
    }
}
