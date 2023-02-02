using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.ViewModels.ShopProduct;

namespace RMLXCast.Web.ViewModelsFactories.ShopProducts
{
    public interface IShopProductViewModelFactory
    {
        ShopProductDetailViewModel CreateShopProductDetailViewModel(Product product);
        ShopProductsPagedViewModel CreateShopProductsPagedViewModel(ICollection<Product> products, ICollection<ProductCategory> productCategories, int pageNumber, int pageSize, int totalProducts, string? searchString);
    }
}