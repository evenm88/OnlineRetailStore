using OnlineRetailStoreApi.Models;
using OnlineRetailStoreApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using OnlineRetailStoreApi.Repository.Interfaces;

namespace OnlineRetailStoreApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly IProductService _productServices;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IServiceProvider serviceProvider, IOrderRepository orderRepository)
        {
            _productServices = serviceProvider.GetRequiredService<IProductService>();
            _orderRepository = orderRepository;
        }
        public void AddOrder(Order order)
        {
            try
            {
                var product = _productServices.GetProduct(order.ProductId);

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
                    _orderRepository.AddOrder(order);
                    var remainingQuantity = product.AvailableQuantity - order.Quantity;
                    _productServices.UpdateQuantity(order.ProductId, remainingQuantity);
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
                    var product = _productServices.GetProduct(order.ProductId);
                    _orderRepository.DeleteOrder(orderId);
                    var remainingQuantity = product.AvailableQuantity + order.Quantity;
                    _productServices.UpdateQuantity(product.ProductId, remainingQuantity);

                }
            }
            catch
            {
                throw;
            }

        }

        public Order GetOrder(string orderId)
        {
            return _orderRepository.GetOrder(orderId);
        }

        public List<Order> GetOrderList()
        {
            return _orderRepository.GetAllOrders();
        }
    }
}
