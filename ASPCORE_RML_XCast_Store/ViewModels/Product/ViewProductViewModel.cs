using RMLXCast.Core.Domain.Catalog;

namespace RMLXCast.Web.ViewModels.Product
{
    public class ViewProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ShortDescription { get; set; }
        public decimal Price { get; set; }

        public bool Published { get; set; }

        public int Stock { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
