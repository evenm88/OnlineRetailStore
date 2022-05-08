using MongoDB.Driver;
using OnlineRetailStoreApi.Models;
using OnlineRetailStoreApi.DbSettings;
using OnlineRetailStoreApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRetailStoreApi.Services
{
    public class ProductService : IProductService
    {

        private readonly IMongoCollection<Product> _products;
        public ProductService(IOnlineRetailDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _products = database.GetCollection<Product>(settings.ProductCollectionName);
        }
        public void AddProduct(Product product)
        {
            _products.InsertOne(product);
        }

        public void DeleteProduct(string productId)
        {
            _products.DeleteOne(product => product.ProductId == productId);
        }
       

        public Product GetProduct(string productId)
        {
            return _products.Find<Product>(product => product.ProductId == productId).FirstOrDefault();
        }

        public List<Product> GetProductList()
        {
            return _products.Find(product => true).ToList();
        }
        public void UpdateQuantity(string productId, int quantity)
        {
            var product = _products.Find<Product>(product => product.ProductId == productId).FirstOrDefault();
            product.AvailableQuantity = quantity;
            _products.ReplaceOne(product => product.ProductId == productId, product);
        }
    }
}
