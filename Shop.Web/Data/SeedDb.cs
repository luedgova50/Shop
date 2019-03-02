namespace Shop.Web.Data
{

    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Shop.Web.Helpers;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class SeedDb
    {
        private readonly DataContext context;

        private readonly IUserHelper userHelper;

        private readonly Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;

            this.userHelper = userHelper;

            random = new Random();
        }

        public async Task SeedAsync()
        {
            await context.Database.EnsureCreatedAsync();

            var user =
                await userHelper.GetUserByEmailAsync("luedgova1967@gmail.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Luis Eduardo",
                    LastName = "Gonzàlez Vargas",
                    Email = "luedgova1967@gmail.com",
                    UserName = "luedgova1967@gmail.com",
                    PhoneNumber = "3133532295"

                };

                var result = await userHelper.AddUserAsync(user, "Luis67103050&");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }


            if (!context.Products.Any())
            {
                AddProduct("Plana", user);
                AddProduct("Fileteadora", user);
                AddProduct("Collarin", user);
                AddProduct("20 U", user);

                await context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            context.Products.Add(new Product
            {
                Name = name,
                Price = random.Next(1000000),
                IsAvailabe = true,
                Stock = random.Next(100),
                User = user
            });
        }

    }
}
