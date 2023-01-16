using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Core.Domain.Catalog
{
    public class ProductCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool Editable { get; set; }

        public IList<Product> Products { get; set; } = new List<Product>();

        public ICollection<ProductProductCategory> ProductProductCategories { get; set; } = null!;
    }
}
