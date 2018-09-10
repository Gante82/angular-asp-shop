using AngularAspShop.Data;
using AngularAspShop.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularAspShop.Data
{
    public class ShopSeeder
    {
        private readonly ShopContext context;
        private readonly IHostingEnvironment hosting;
        private readonly UserManager<ShopUser> userManager;

        public ShopSeeder(ShopContext context, IHostingEnvironment hosting, UserManager<ShopUser> userManager)
        {
            this.context = context;
            this.hosting = hosting;
            this.userManager = userManager;
        }

        public async Task SeedAsync()
        {
            // Makes sure the database exists
            context.Database.EnsureCreated();
            var user = await userManager.FindByEmailAsync("rmarin82@gmail.com");
            if (user == null)
            {
                user = new ShopUser()
                {
                    FirstName = "Raul",
                    LastName = "Marin",
                    Email = "rmarin82@gmail.com",
                    UserName = "rmarin82@gmail.com"
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd!");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

            if (!context.Products.Any())
            {
                var file = Path.Combine(hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(file);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

                context.Products.AddRange(products);

                var order = context.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if (order != null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }

                context.SaveChanges();
            }
        }
    }
}
