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
    }
}
