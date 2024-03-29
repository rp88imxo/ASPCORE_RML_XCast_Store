﻿using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.ViewModels.Product;

namespace RMLXCast.Web.ViewModelsFactories.ProductFactory
{
    public interface IProductViewModelFactory
    {
        ProductsPagedViewModel CreateProductPagedViewModel(ICollection<Product> products, int pageNumber, int pageSize, int totalProducts);
		Task<CreateProductViewModel> GetCreateProductViewModelAsync();
        Task<EditProductViewModel> GetEditProductViewModelAsync(Product product);
        Task UpdateCreateProductViewModelAsync(CreateProductViewModel viewModel);
        Task UpdateEditProductViewModelAsync(EditProductViewModel viewModel);
    }
}