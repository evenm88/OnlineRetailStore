using MongoDB.Driver;
using OnlineRetailStoreApi.DbSettings;
using OnlineRetailStoreApi.Models;
using OnlineRetailStoreApi.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRetailStoreApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IOnlineRetailDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);

            var database = client.GetDatabase(dbSettings.DatabaseName);

            _products = database.GetCollection<Product>(dbSettings.ProductCollectionName);
        }
        public void AddProduct(Product product)
        {
            _products.InsertOne(product);
        }

        public void DeleteProduct(string productId)
        {
            _products.DeleteOne(product=>product.ProductId==productId);
        }

        public List<Product> GetAllProducts()
        {
            return _products.Find(product => true).ToList();
        }

        public Product GetProduct(string productId)
        {
            return _products.Find(product => product.ProductId == productId).FirstOrDefault();
        }

        public void UpdateProductQuantity(string productId, int quantity)
        {
            var product = GetProduct(productId);
            product.AvailableQuantity = quantity;
            _products.ReplaceOne(product => product.ProductId == productId, product);
        }
    }
}
