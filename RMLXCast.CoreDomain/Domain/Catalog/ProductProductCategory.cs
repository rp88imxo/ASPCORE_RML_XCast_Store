using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Core.Domain.Catalog
{
    public class ProductProductCategory
    {
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory? ProductCategory { get; set; }

    }
}
