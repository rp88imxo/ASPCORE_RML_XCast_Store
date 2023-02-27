namespace RMLXCast.Web.ViewModels.Checkout
{
    public class CheckoutViewProductViewModel
    {
        public string ProductName { get; set;} = string.Empty;
        public int Amount { get; set;}
        public decimal PricePerSlot { get; set; }
    }
}
