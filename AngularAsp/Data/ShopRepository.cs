using AngularAspShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularAspShop.Data
{
    public class ShopRepository : IShopRepository
    {
        private readonly ShopContext context;
        private readonly ILogger<ShopRepository> logger;

        public ShopRepository(ShopContext context, ILogger<ShopRepository> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }

            return context.Orders
                .ToList();
        }

        public Order GetOrderById(string username, int id)
        {


            return context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.Id == id && o.User.UserName == username)
                .FirstOrDefault();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            logger.LogInformation("GetAllProducts was called");
            return context.Products;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return context.Products.Where(p => p.Category == category);
        }

        public bool SaveAll()
        {
            return context.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            context.Add(model);
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            var result = context.Orders.Where(o => o.User.UserName == username);

            if (includeItems)
            {
                return result
                    .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }

            return result
                .ToList();
        }
    }
}
