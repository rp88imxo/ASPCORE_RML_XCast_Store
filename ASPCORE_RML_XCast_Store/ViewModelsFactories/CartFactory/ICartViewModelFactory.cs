using RMLXCast.Core.Domain.Cart;
using RMLXCast.Web.ViewModels.Cart;

namespace RMLXCast.Web.ViewModelsFactories.CartFactory
{
    public interface ICartViewModelFactory
    {
        Task<CartViewModel> GetCartViewModelAsync(IList<CartProduct> cartProducts);
    }
}