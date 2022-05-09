using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OnlineRetailStoreApi.Controllers;
using OnlineRetailStoreApi.Models;
using OnlineRetailStoreApi.Repository.Interfaces;
using OnlineRetailStoreApi.Services;
using OnlineRetailStoreApi.Services.Interfaces;

namespace OnlineRetailStoreTest
{
    public class Tests
    {

        private List<Product> _products;
        private List<Order> _orders;
        private Mock<IOrderRepository> _orderRepository;
        private Mock<IProductRepository> _productRepository;
        private IProductService _productService;
        private IOrderService _orderService;
        private ProductController _productController;
        private OrderController _orderController;

        public Tests()
        {
        }

        [SetUp]
        public void Setup()
        {
            _products = new List<Product>
            {
                new Product()
                {
                    ProductId="PRO0001",
                    ProductName="Pepsi",
                    ProductPrice=15,
                    AvailableQuantity=50
                },
                new Product()
                {
                    ProductId="PRO0002",
                    ProductName="7up",
                    ProductPrice=10,
                    AvailableQuantity=100
                },
                new Product()
                {
                    ProductId="PRO0003",
                    ProductName="Redbull",
                    ProductPrice=60,
                    AvailableQuantity=25
                },
            };

            _orders = new List<Order>
            {
                new Order()
                {
                   OrderId="Order0001",
                   ProductId="PRO0002",
                   Quantity = 1,
                   BillAmount= 10
                },

                new Order()
                {
                   OrderId="Order0002",
                   ProductId="PRO0003",
                   Quantity = 2,
                   BillAmount= 120
                }
            };

            _orderRepository = new Mock<IOrderRepository>();
            _productRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepository.Object);
            _productController = new ProductController(_productService);
            _orderService = new OrderService(_orderRepository.Object, _productService);
            _orderController = new OrderController(_orderService);
        }

        [TestCase("PRO0001")]
        public void GetProductSuccessTest(string id)
        {
            _productRepository.Setup(x => x.GetProduct(id))
               .Returns(_products.FirstOrDefault(product => product.ProductId == id));
            var result = _productController.GetProduct(id) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode.Value);
        }

        [TestCase("PRO00011")]
        public void GetProductFailTest(string id)
        {
            _productRepository.Setup(x => x.GetProduct(id))
               .Returns(_products.FirstOrDefault(product => product.ProductId == id));
            var result = _productController.GetProduct(id) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode.Value);
        }

        [TestCase("Order0001")]
        public void GetOrderSuccessTest(string id)
        {
            _orderRepository.Setup(x => x.GetOrder(id))
              .Returns(_orders.FirstOrDefault(order => order.OrderId == id));
            var result = _orderController.GetOrder(id) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode.Value);
        }

        [TestCase("Order00011")]
        public void GetOrderFailTest(string id)
        {
            _orderRepository.Setup(x => x.GetOrder(id))
              .Returns(_orders.FirstOrDefault(order => order.OrderId == id));
            var result = _orderController.GetOrder(id) as ObjectResult;
            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode.Value);
        }

    }
}