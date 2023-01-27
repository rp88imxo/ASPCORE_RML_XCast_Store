using Microsoft.AspNetCore.Hosting;
using RMLXCast.Core.Domain.Catalog;

namespace RMLXCast.Web.Services.ProductImagesService
{
    // TODO: Rework this later
    public class ProductImagesService : IProductImagesService
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductImagesService(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public IList<string> GetAllProductImagesUrls(Product product)
        {
            var webRootPath = webHostEnvironment.WebRootPath;
           
            var loadPath = GetPhysicalPathForProductImage(product);

            if (!Directory.Exists(loadPath))
            {
                return new List<string>() 
                { 
                    GetDefaultProductImage() 
                };
            }

            var imagePaths = Directory.GetFiles(loadPath);

            var resultImageUrls = new List<string>();

            foreach (var imagePath in imagePaths)
            {
                var name = Path.GetFileName(imagePath);
                string imageWebPath = GetVirtualPathForProductImage(product, name);
                resultImageUrls.Add(imageWebPath);
            }

            return resultImageUrls;
        }

        private string GetVirtualPathForProductImage(Product product, string name)
        {
            return Path.Combine(
                "ExternalFiles",
                "Products",
                $"{product.Id}",
                name);
        }

        private string GetDefaultProductImage()
        {
            var loadPath = Path.Combine(
               "ExternalFiles",
               "ProductDefaultImage",
               "DefaultImage.png");

            return loadPath;
        }

        public string? GetMainProductImageUrl(Product product)
        {
            var allProductImagesUrls = GetAllProductImagesUrls(product);
            var mainImageUrl = allProductImagesUrls.FirstOrDefault();

            return mainImageUrl;
        }


        public async Task SaveProductImagesAsync(Product product, List<IFormFile>? productImages)
        {
            if (productImages == null || productImages.Count == 0)
            {
                return;
            }

            var savePath = GetPhysicalPathForProductImage(product);

            Directory.CreateDirectory(savePath);

            var imagePaths = Directory.GetFiles(savePath);

            foreach (var imagePath in imagePaths)
            {
                File.Delete(imagePath);
            }

            foreach (var productImage in productImages)
            {
                var imagePath = Path.Combine(savePath, productImage.FileName);

                if (File.Exists(imagePath))
                {
                    continue;
                }

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await productImage.CopyToAsync(fileStream);
                }
            }
        }

        private string GetPhysicalPathForProductImage(Product product)
        {
            var webRootPath = webHostEnvironment.WebRootPath;
            var savePath = Path.Combine(
                webRootPath,
                "ExternalFiles",
                "Products",
                $"{product.Id}");

            return savePath;
        }
    }
}
