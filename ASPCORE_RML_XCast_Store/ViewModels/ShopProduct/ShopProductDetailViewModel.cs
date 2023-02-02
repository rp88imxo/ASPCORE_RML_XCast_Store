namespace RMLXCast.Web.ViewModels.ShopProduct
{
    public class ShopProductDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ShortDescription { get; set; }
        public string? FullDescription { get; set; }
        public string PriceFormated { get; set; } = null!;
        public int StockAmount { get; set; }
        public IList<string> ProductImageUrls { get; set; } = new List<string>();
    }
}
