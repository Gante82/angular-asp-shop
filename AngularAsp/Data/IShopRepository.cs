using System.Collections.Generic;
using AngularAspShop.Data.Entities;

namespace AngularAspShop.Data
{
    public interface IShopRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
        IEnumerable<Order> GetAllOrders(bool includeItems);
        Order GetOrderById(string name, int id);
        void AddEntity(object model);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
    }
}