using Microsoft.EntityFrameworkCore;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Services.Catalog
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        #region DEFAULT_CRUD

        public async Task CreateProductAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId, bool includeStocks)
        {
            var products = dbContext.Products;

            Product? product;

            if (includeStocks)
            {
                product = await products
                     .Include(x => x.Stocks)
                     .Include(x => x.ProductCategories)
                .FirstOrDefaultAsync(x => x.Id == productId);
            }
            else
            {
                product = await products
                .Include(x => x.ProductCategories)
                .FirstOrDefaultAsync(x => x.Id == productId);
            }

            return product;
        }

        public async Task<IList<Product>> GetAllProductsAsync()
        {
            return await dbContext.Products.ToListAsync();
        }

        public async Task<IList<Product>> GetPagedProductsAsync(int pageNumber, int pageSize)
        {
            return await dbContext.Products
                .Include(x => x.Stocks)
                .Include(x => x.ProductCategories)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IList<Product>> GetPagedProductsAsync(int pageNumber, int pageSize, string searchString, ProductCategory? productCategory, bool publishedOnly = true)
        {
            IQueryable<Product> query = dbContext.Products
                .Include(x => x.Stocks)
                .Include(x => x.ProductCategories);

            if (productCategory != null)
            {
                query = query.Where(x => x.ProductCategories.Any(x => x.Id == productCategory.Id));
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.Contains(searchString) || x.ShortDescription!.Contains(searchString));
            }

            return await query
                .Where(x=> x.Published)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalProductCountAsync()
        {
            return await dbContext.Products.CountAsync();
        }

        public async Task<int> GetTotalProductCountByCategoryAsync(int productCategoryId)
        {
            return await dbContext.Products
                .Where(x => x.ProductCategories.Any(x => x.Id == productCategoryId))
                .CountAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductByIdAsync(int productId)
        {
            var product = await GetProductByIdAsync(productId, false);
            if (product == null)
            {
                return false;
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return true;
        }

        #endregion
    }
}
