using RMLXCast.Core.Domain.Catalog;

namespace RMLXCast.Web.Services.ProductImagesService
{
    public interface IProductImagesService
    {
        IList<string> GetAllProductImagesUrls(Product product);
        string? GetMainProductImageUrl(Product product);
        void HandleProductDeleted(Product product);
        public Task SaveProductImagesAsync(Product product, List<IFormFile>? productImages);
    }
}
