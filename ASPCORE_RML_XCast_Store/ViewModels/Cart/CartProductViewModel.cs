namespace RMLXCast.Web.ViewModels.Cart
{
    public class CartProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal PricePerSlot { get; set; }
        public int Amount { get; set; }
        public int OrderMinimumQuantity { get; set; }

        public int OrderMaximumQuantity { get; set; }

    }
}
