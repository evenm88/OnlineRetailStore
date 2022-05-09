using OnlineRetailStoreApi.Models;
using System.Collections.Generic;

namespace OnlineRetailStoreApi.Repository.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();
        Product GetProduct(string productId);
        void AddProduct(Product product);
        void DeleteProduct(string productId);
        void UpdateProductQuantity(string productId, int quantity);
    }
}
