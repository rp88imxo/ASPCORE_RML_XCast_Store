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

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await dbContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
        }

        public async Task UpdateProductAsync(Product product)
        {
            dbContext.Products.Update(product);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductByIdAsync(int productId)
        {
            var product = await GetProductByIdAsync(productId);
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
