using RMLXCast.Core.Domain.Cart;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Core.Domain.User;
using RMLXCast.Web.ViewModels.Checkout;

namespace RMLXCast.Web.ViewModelsFactories.CheckoutFactory
{
    public interface ICheckoutViewModelFactory
    {
        CheckoutProductsViewModel CreateCheckoutProductsViewModel(ApplicationUser applicationUser, IList<Product> products, IList<CartProduct> cartProducts);
    }
}