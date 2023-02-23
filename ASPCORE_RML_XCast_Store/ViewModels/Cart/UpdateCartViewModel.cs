namespace RMLXCast.Web.ViewModels.Cart
{
    public class UpdateCartViewModel
    {
        public IList<CartProductViewModel> SessionCartProducts { get; set; } = new List<CartProductViewModel>();
    }
}
