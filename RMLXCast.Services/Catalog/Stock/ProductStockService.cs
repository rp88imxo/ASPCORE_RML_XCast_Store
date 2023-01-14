using Microsoft.EntityFrameworkCore;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Services.Catalog.Stocks
{
    public class ProductStockService : IProductStockService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public ProductStockService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<IList<Stock>?> GetAllStockForProduct(int productId)
        {
            var stocks = await applicationDbContext.Stocks.Where(x => x.ProductId == productId).ToListAsync();

            return stocks;
        }
    }
}
