using PagedList.Core;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.Services.ProductImagesService;
using RMLXCast.Web.ViewModels.ShopProduct;

namespace RMLXCast.Web.ViewModelsFactories.ShopProducts
{
    public class ShopProductViewModelFactory : IShopProductViewModelFactory
    {
        private readonly IProductImagesService productImagesService;

        public ShopProductViewModelFactory(
            IProductImagesService productImagesService
            )
        {
            this.productImagesService = productImagesService;
        }

        public ShopProductDetailViewModel CreateShopProductDetailViewModel(Product product)
        {
            var model = new ShopProductDetailViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                PriceFormated = GetFormattedPrice(product.Price),
                ProductImageUrls = productImagesService.GetAllProductImagesUrls(product),
                StockAmount = product.Stocks.Sum(x => x.StockQuantity)
            };

            return model;
        }

        public ShopProductsPagedViewModel CreateShopProductsPagedViewModel(ICollection<Product> products,
            ICollection<ProductCategory> productCategories,
            int pageNumber,
            int pageSize,
            int totalProducts,
            string? searchString)
        {
            var model = new ShopProductsPagedViewModel()
            {
                PageNumber = pageNumber,
                searchString = searchString,
                viewShopProductViewModels = new StaticPagedList<ViewShopProductViewModel>(products.Select(x => new ViewShopProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    PriceFormated = GetFormattedPrice(x.Price),
                    ShortDescription = x.ShortDescription,
                    productImageUrl = productImagesService.GetMainProductImageUrl(x)
                }),
                pageNumber, pageSize, totalProducts),
                shopProductCategoryViewModels = productCategories
                .Select(x => new ShopProductCategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToList()
            };

            return model;
        }

        // TODO: Move to service
        private string GetFormattedPrice(decimal price)
        {
            return string.Format("{0:G0} ₽", price);
        }
    }
}
