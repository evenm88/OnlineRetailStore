using OnlineRetailStoreApi.Models;
using OnlineRetailStoreApi.Services.Interfaces;
using System.Collections.Generic;
using OnlineRetailStoreApi.Repository.Interfaces;

namespace OnlineRetailStoreApi.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
        }

        public void DeleteProduct(string productId)
        {
            _productRepository.DeleteProduct(productId);
        }
        public Product GetProduct(string productId)
        {
            return _productRepository.GetProduct(productId);
        }

        public List<Product> GetProductList()
        {
            return _productRepository.GetAllProducts();
        }

        public void UpdateQuantity(string productId, int quantity)
        {
            _productRepository.UpdateProductQuantity(productId, quantity);
        }
    }
}
