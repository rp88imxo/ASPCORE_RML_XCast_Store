namespace RMLXCast.Web.ViewModels.Cart
{
    public class CartViewModel
    {
        public IList<CartProductViewModel> CartProducts { get; set; } = new List<CartProductViewModel>();
    }
}
