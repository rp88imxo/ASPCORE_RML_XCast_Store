using PagedList.Core;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Services.Catalog;
using RMLXCast.Services.Catalog.Category;
using RMLXCast.Web.ViewModels.Product;

namespace RMLXCast.Web.ViewModelsFactories.ProductFactory
{
    public class ProductViewModelFactory : IProductViewModelFactory
    {
        private readonly IProductService productService;
        private readonly IProductCategoryService productCategoryService;

        public ProductViewModelFactory(
            IProductService productService,
            IProductCategoryService productCategoryService)
        {
            this.productService = productService;
            this.productCategoryService = productCategoryService;
        }

        public ProductsPagedViewModel CreateProductPagedViewModel(
            ICollection<Product> products,
            int pageNumber,
            int pageSize,
            int totalProducts)
        {
            var model = new ProductsPagedViewModel()
            {
                PageNumber = pageNumber,
                ViewProductViewModels = new StaticPagedList<ViewProductViewModel>(products.Select(x => new ViewProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ShortDescription = x.ShortDescription,
                    Price = x.Price,
                    ProductCategories = x.ProductCategories,
                    Stock = CalculateTotalStock(x.Stocks),
                    Published = x.Published
                }), pageNumber, pageSize, totalProducts)
            };

            return model;
        }

        public async Task UpdateCreateProductViewModelAsync(CreateProductViewModel viewModel)
        {
            var categories = await productCategoryService.GetAllProductCategoriesAsync();

            viewModel.AllProductCategoriesSelectListItems = categories
                .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();
        }

        public async Task<EditProductViewModel> GetEditProductViewModel(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product can't be null!");
            }

            var categories = await productCategoryService.GetAllProductCategoriesAsync();

            var model = new EditProductViewModel()
            {
                Id= product.Id,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                AdminComment= product.AdminComment,
                AllowCustomerReviews= product.AllowCustomerReviews,
                Price= product.Price,
                OrderMinimumQuantity= product.OrderMinimumQuantity,
                OrderMaximumQuantity= product.OrderMaximumQuantity,
                Published= product.Published,
                Stock = 

                AllProductCategoriesSelectListItems = categories
                .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList()
            };
        }

        public async Task<CreateProductViewModel> GetCreateProductViewModelAsync()
        {
            var categories = await productCategoryService.GetAllProductCategoriesAsync();

            return new CreateProductViewModel()
            {
                Stock = 1000,
                Published = true,
                AllProductCategoriesSelectListItems = categories
                .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList()
            };
        }

        private int CalculateTotalStock(ICollection<Stock> stocks)
        {
            if (stocks == null)
            {
                throw new ArgumentNullException("Stocks has been null!");
            }

            return stocks.Sum(x => x.StockQuantity);
        }
    }
}
