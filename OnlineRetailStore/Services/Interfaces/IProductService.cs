using OnlineRetailStoreApi.Models;
using System.Collections.Generic;

namespace OnlineRetailStoreApi.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProductList();
        Product GetProduct(string productId);
        void AddProduct(Product product);
        void UpdateQuantity(string productId,int quantity);
        void DeleteProduct(string productId);
    }
}
