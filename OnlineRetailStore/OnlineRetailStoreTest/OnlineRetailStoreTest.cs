using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using OnlineRetailStoreApi.Controllers;
using OnlineRetailStoreApi.Models;
using OnlineRetailStoreApi.Services.Interfaces;

namespace OnlineRetailStoreTest
{
    public class Tests
    {
        private List<Product> _products;
        private List<Order> _orders;
        private readonly Mock<IProductService> _productService;
        private readonly Mock<IOrderService> _orderService;
        private readonly ProductController _productController;
        private readonly OrderController _orderController;

        public Tests()
        {
            _productService = new Mock<IProductService>();
            _productController = new ProductController(_productService.Object);
            _orderService = new Mock<IOrderService>();
            _orderController = new OrderController(_orderService.Object);
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
               }
           };
        }

        [TestCase("PRO0001")]
        public void GetProductSuccessTest(string id)
        {
            _productService.Setup(x => x.GetProduct(id))
                .Returns(_products.FirstOrDefault(product => product.ProductId == id));

            var result = _productController.GetProduct(id) as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode.Value);
        }

        [TestCase("PRO00011")]
        public void GetProductFailTest(string id)
        {
            _productService.Setup(x => x.GetProduct(id))
                .Returns(_products.FirstOrDefault(product => product.ProductId == id));

            var result = _productController.GetProduct(id) as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode.Value);
        }

        [TestCase("Order0001")]
        public void GetOrderSuccessTest(string id)
        {
            _orderService.Setup(x => x.GetOrder(id))
                .Returns(_orders.FirstOrDefault(order => order.OrderId == id));

            var result = _orderController.GetOrder(id) as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode.Value);
        }

        [TestCase("Order00011")]
        public void GetOrderFailTest(string id)
        {
            _orderService.Setup(x => x.GetOrder(id))
                .Returns(_orders.FirstOrDefault(order => order.OrderId == id));

            var result = _orderController.GetOrder(id) as ObjectResult;

            Assert.AreEqual((int)HttpStatusCode.NotFound, result.StatusCode.Value);
        }
    }
}