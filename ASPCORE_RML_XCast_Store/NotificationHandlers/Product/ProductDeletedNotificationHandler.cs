using MediatR;
using RMLXCast.Services.Notifications;
using RMLXCast.Web.Services.ProductImagesService;

namespace RMLXCast.Web.NotificationHandlers.Product
{
    public class ProductDeletedNotificationHandler : INotificationHandler<ProductDeletedNotification>
    {
        private readonly IProductImagesService productImageService;

        public ProductDeletedNotificationHandler(IProductImagesService productImageService)
        {
            this.productImageService = productImageService;
        }


        public Task Handle(ProductDeletedNotification notification, CancellationToken cancellationToken)
        {
            productImageService.HandleProductDeleted(notification.Product);

            return Task.CompletedTask;
        }
    }
}
