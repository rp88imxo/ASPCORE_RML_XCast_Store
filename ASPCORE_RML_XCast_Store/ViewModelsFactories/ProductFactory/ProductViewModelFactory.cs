﻿using PagedList.Core;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Services.Catalog;
using RMLXCast.Services.Catalog.Category;
using RMLXCast.Services.Catalog.Stocks;
using RMLXCast.Web.ViewModels.Product;

namespace RMLXCast.Web.ViewModelsFactories.ProductFactory
{
    public class ProductViewModelFactory : IProductViewModelFactory
    {
        private readonly IProductService productService;
        private readonly IProductCategoryService productCategoryService;
        private readonly IProductStockService productStockService;

        public ProductViewModelFactory(
            IProductService productService,
            IProductCategoryService productCategoryService,
            IProductStockService productStockService)
        {
            this.productService = productService;
            this.productCategoryService = productCategoryService;
            this.productStockService = productStockService;
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
                    ShortDescription = GetClampedDescription(x.ShortDescription, 36),
                    Price = x.Price,
                    ProductCategories = x.ProductCategories,
                    Stock = CalculateTotalStock(x.Stocks),
                    Published = x.Published
                }), pageNumber, pageSize, totalProducts)
            };

            return model;
        }

        private string? GetClampedDescription(string? description, int maxChars)
        {
            if (description == null)
            {
                return null;
            }

            maxChars = Math.Clamp(maxChars, 0, description.Length);

            var resultString = description.Substring(0, maxChars);

            return resultString;
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

        public async Task<EditProductViewModel> GetEditProductViewModelAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Product can't be null!");
            }

            var categories = await productCategoryService.GetAllProductCategoriesAsync();
            var stock = await productStockService.GetAllStockForProductAsync(product.Id);

            var selectedIds = product.ProductCategories.Select(x => x.Id).ToList();

            var model = new EditProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                AdminComment = product.AdminComment,
                AllowCustomerReviews = product.AllowCustomerReviews,
                Price = product.Price,
                OrderMinimumQuantity = product.OrderMinimumQuantity,
                OrderMaximumQuantity = product.OrderMaximumQuantity,
                Published = product.Published,
                Stock = stock!.Sum(x => x.StockQuantity),
                SelectedProductCategoriesIds = selectedIds,
                AllProductCategoriesSelectListItems = categories
                .Select(x => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                    Selected = selectedIds.Contains(x.Id)
                })
                .ToList()
            };

            return model;
        }

        public async Task UpdateEditProductViewModelAsync(EditProductViewModel viewModel)
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
