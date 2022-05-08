using MongoDB.Driver;
using OnlineRetailStoreApi.Models;
using OnlineRetailStoreApi.DbSettings;
using OnlineRetailStoreApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineRetailStoreApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMongoCollection<Order> _orders;
        private readonly IProductService productServices;

        public OrderService(IServiceProvider serviceProvider, IOnlineRetailDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _orders = database.GetCollection<Order>(settings.OrderCollectionName);
            productServices = serviceProvider.GetRequiredService<IProductService>();
        }
        public void AddOrder(Order order)
        {
            try
            {
                var product = productServices.GetProduct(order.ProductId);

                if (product == null)
                {
                    throw new Exception("Product not exist");
                }
                else if (product.AvailableQuantity < order.Quantity)
                {
                    throw new Exception("Requested qty not available");
                }
                else
                {
                    order.BillAmount = order.Quantity * product.ProductPrice;
                    _orders.InsertOne(order);
                    var remainingQuantity = product.AvailableQuantity - order.Quantity;
                    productServices.UpdateQuantity(order.ProductId, remainingQuantity);
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteOrder(string orderId)
        {
            try
            {
                var order = GetOrder(orderId);

                if (order == null)
                {
                    throw new Exception("Order not exist");
                }
                else
                {
                    var product = productServices.GetProduct(order.ProductId);
                    _orders.DeleteOne(order => order.OrderId == orderId);
                    var remainingQuantity = product.AvailableQuantity + order.Quantity;
                    productServices.UpdateQuantity(product.ProductId, remainingQuantity);

                }
            }
            catch
            {
                throw;
            }

        }


        public Order GetOrder(string orderId)
        {
            return _orders.Find<Order>(order => order.OrderId == orderId).FirstOrDefault();
        }

        public List<Order> GetOrderList()
        {
            return _orders.Find(order => true).ToList();
        }
    }
}
