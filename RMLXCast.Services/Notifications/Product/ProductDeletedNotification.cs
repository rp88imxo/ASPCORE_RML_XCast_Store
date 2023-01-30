using MediatR;
using RMLXCast.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Services.Notifications
{
    public class ProductDeletedNotification : INotification
    {
        public Product Product { get; } = null!;

        public ProductDeletedNotification(Product product)
        {
            Product = product;
        }
    }
}
