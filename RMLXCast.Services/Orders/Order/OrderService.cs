﻿using RMLXCast.Core.Domain.Cart;
using RMLXCast.Core.Domain.Catalog;
using RMLXCast.Core.Domain.Orders;
using RMLXCast.Core.Domain.ShippmentAddress;
using RMLXCast.Core.Domain.User;
using RMLXCast.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMLXCast.Services.Orders.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public OrderService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task CreateOrderAsync(IEnumerable<Product> products, IEnumerable<CartProduct> cartProducts, ApplicationUser applicationUser, Address address)
        {
            var order = new Order()
            {
                ApplicationUser = applicationUser,
                CreatedOnUtc = DateTime.UtcNow,
                OrderGuid = Guid.NewGuid(),
                OrderStatus = OrderStatus.Pending,
                Address = address
            };

            var orderedItems = new List<OrderItem>(products.Count());

            foreach (var product in products)
            {
                foreach (var cartProduct in cartProducts)
                {
                    if (cartProduct.Id == product.Id)
                    {
                        orderedItems.Add(new OrderItem()
                        {
                            Order = order,
                            Product = product,
                            Quantity = cartProduct.Id
                        });
                    }
                }
            }

            order.OrderItems = orderedItems;

            await applicationDbContext.Orders.AddAsync(order);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}
