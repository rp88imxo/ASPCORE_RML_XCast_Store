using Microsoft.AspNetCore.Hosting;
using RMLXCast.Core.Domain.Catalog;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace RMLXCast.Web.Services.ProductImagesService
{
    // TODO: Rework this later
    public class ProductImagesService : IProductImagesService
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger<ProductImagesService> logger;

        private readonly int _requiredWidth = 600;
        private readonly int _requiredHeight = 750;

        public ProductImagesService(
            IWebHostEnvironment webHostEnvironment,
            ILogger<ProductImagesService> logger)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
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

            var saveTasks = new List<Task>();
            foreach (var productImage in productImages)
            {
                saveTasks.Add(Task.Run(async () =>
                {
                    await SaveProductImageAsync(savePath, productImage);
                }));
            }

            await Task.WhenAll(saveTasks);
        }

        private async Task SaveProductImageAsync(string savePathDir, IFormFile productImage)
        {
            var imagePath = Path.Combine(savePathDir, productImage.FileName);

            if (File.Exists(imagePath))
            {
                return;
            }

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                var stream = productImage.OpenReadStream();

                var loadedImage = await Image.LoadAsync(stream);

                if (loadedImage == null)
                {
                    return;
                }

                var defaultWidth = loadedImage.Width;
                var defaultHeight = loadedImage.Height;
                //var aspectRatio = (float)defaultWidth / defaultHeight;

                if (defaultWidth > _requiredWidth)
                {
                    loadedImage.Mutate(op =>
                    {
                        op.Resize(_requiredWidth, 0); 
                    });
                }
                else if (defaultHeight > _requiredHeight)
                {
                    loadedImage.Mutate(op =>
                    {
                        op.Resize(0, _requiredHeight);
                    });
                }

                await loadedImage.SaveAsJpegAsync(fileStream, new JpegEncoder()
                {
                    Quality = 75
                }); 
            }
        }

        public void HandleProductDeleted(Product product)
        {
            var path = GetPhysicalPathForProductImage(product);

            if (!Directory.Exists(path))
            {
                return;
            }

            Directory.Delete(path, true);
            logger.Log(LogLevel.Information, $"Deleted product image files for product: {product.Name}");
        }

        private string GetPhysicalPathForProductImage(Product product)
        {
            var webRootPath = webHostEnvironment.WebRootPath;
            var path = Path.Combine(
                webRootPath,
                "ExternalFiles",
                "Products",
                $"{product.Id}");

            return path;
        }
    }
}
