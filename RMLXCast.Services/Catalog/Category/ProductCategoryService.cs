using Microsoft.EntityFrameworkCore;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Services.Catalog.Category
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ProductCategoryService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IList<ProductCategory>> GetAllProductCategoriesAsync()
        {
            return await applicationDbContext.ProductCategories.ToListAsync();
        }

        public async Task CreateProductCategoryAsync(ProductCategory productCategory)
        {
            await applicationDbContext.ProductCategories.AddAsync(productCategory);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<ProductCategory?> GetProductCategoryByIdAsync(int id)
        {
            var res = await applicationDbContext.ProductCategories.FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }

        public async Task<IList<ProductCategory>> GetPagedProductCategoriesAsync(int pageNumber, int pageSize)
        {
            return await applicationDbContext
                .ProductCategories
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            applicationDbContext.ProductCategories.Update(productCategory);
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductCategoryByIdAsync(int id)
        {
            var product = await GetProductCategoryByIdAsync(id);

            if (product == null || !product.Editable)
            {
                return false;
            }

            applicationDbContext.ProductCategories.Remove(product);
            await applicationDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetTotalProductCountAsync()
        {
            return await applicationDbContext.ProductCategories.CountAsync();
        }
    }
}
