namespace RMLXCast.Web.ViewModels.ShopProduct
{
    public class ViewShopProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ShortDescription { get; set; }
        public string PriceFormated { get; set; } = null!;
        public string? productImageUrl { get; set; }
    }
}
