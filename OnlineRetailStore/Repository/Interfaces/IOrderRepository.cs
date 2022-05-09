using OnlineRetailStoreApi.Models;
using System.Collections.Generic;

namespace OnlineRetailStoreApi.Repository.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order GetOrder(string orderId);
        void AddOrder(Order product);
        void DeleteOrder(string orderId);
    }
}
