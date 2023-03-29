namespace RMLXCast.Web.ViewModels.Orders
{
    public class ViewAdminOrderViewModel
    {
        public int Id { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string OrderStatusFormatted { get; set; }
        public string CreatedOnUtcFormatted { get; set; }
        public string TotalPriceFormatted { get; set; }
    }
}
