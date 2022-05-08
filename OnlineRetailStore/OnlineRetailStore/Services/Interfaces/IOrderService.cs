using OnlineRetailStoreApi.Models;
using System.Collections.Generic;

namespace OnlineRetailStoreApi.Services.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetOrderList();
        Order GetOrder(string orderId);
        void AddOrder(Order order);
        void DeleteOrder(string orderId);
    }


}
