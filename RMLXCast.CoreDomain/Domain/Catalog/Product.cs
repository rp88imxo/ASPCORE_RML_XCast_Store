using RMLXCast.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Core.Domain.Catalog
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ShortDescription { get; set; } = string.Empty;
        public string? FullDescription { get; set; }
        public string? AdminComment { get; set; }

        public bool AllowCustomerReviews { get; set; }

        public decimal Price { get; set; }
       
        public int OrderMinimumQuantity { get; set; }

        public int OrderMaximumQuantity { get; set; }

        public bool Published { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public IList<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();

        public IList<ProductProductCategory> ProductProductCategories { get; set; } = new List<ProductProductCategory>()!;
    }
}
