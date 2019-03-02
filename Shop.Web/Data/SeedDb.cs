namespace Shop.Web.Data
{
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    public class SeedDb
    {
        private readonly DataContext context;

        private Random random;

        public SeedDb(DataContext context)
        {
            this.context = context;

            random = new Random();
        }

        public async Task SeedAsync()
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Products.Any())
            {
                AddProduct("Plana");
                AddProduct("Fileteadora");
                AddProduct("Collarin");
                AddProduct("20 U");
                await context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = random.Next(1000000),
                IsAvailabe = true,
                Stock = random.Next(100)
            });
        }

    }
}
