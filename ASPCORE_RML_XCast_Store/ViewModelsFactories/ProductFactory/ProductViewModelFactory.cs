using PagedList.Core;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Web.ViewModels.Product;

namespace RMLXCast.Web.ViewModelsFactories.ProductFactory
{
    public class ProductViewModelFactory : IProductViewModelFactory
    {
        public ProductsPagedViewModel CreateProductPagedViewModel(
            ICollection<Product> products, 
            int pageNumber,
            int pageSize,
            int totalProducts)
        {
            var model = new ProductsPagedViewModel()
            {
                PageNumber = pageNumber,
                ViewProductViewModels = new StaticPagedList<ViewProductViewModel>(products.Select(x=> new ViewProductViewModel
                {
                Id= x.Id,
                Name= x.Name,
                ShortDescription = x.ShortDescription,
                Price= x.Price,
                ProductCategories= x.ProductCategories,
                Stock = CalculateTotalStock(x.Stocks),
                Published= x.Published
                }), pageNumber, pageSize, totalProducts)
            };

            return model;
        }

		public CreateProductViewModel GetCreateProductViewModel()
		{
			return new CreateProductViewModel()
			{
				Stock = 1000,
				Published = true
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
