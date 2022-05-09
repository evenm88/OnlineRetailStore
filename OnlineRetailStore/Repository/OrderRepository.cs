using MongoDB.Driver;
using OnlineRetailStoreApi.DbSettings;
using OnlineRetailStoreApi.Models;
using OnlineRetailStoreApi.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OnlineRetailStoreApi.Repository
{
    public class OrderRepository:IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IOnlineRetailDatabaseSettings dbSettings)
        {
            var client = new MongoClient(dbSettings.ConnectionString);

            var database = client.GetDatabase(dbSettings.DatabaseName);

            _orders = database.GetCollection<Order>(dbSettings.OrderCollectionName);
        }

        public void AddOrder(Order order)
        {
            _orders.InsertOne(order);
        }

        public void DeleteOrder(string orderId)
        {
            _orders.DeleteOne(order => order.OrderId == orderId);
        }
        public List<Order> GetAllOrders()
        {
            return _orders.Find(order => true).ToList();
        }

        public Order GetOrder(string orderId)
        {
            return _orders.Find(order => order.OrderId == orderId).FirstOrDefault();
        }

        
    }
}
