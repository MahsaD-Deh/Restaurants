
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Model;

namespace Restaurants.Infrastructure.Seeders
{
    internal class RestaurantSeeder(RestaurantsDbContext dbContext) : IRestaurantSeeder
    {
        public async Task Seed()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    dbContext.Restaurants.AddRange(restaurants);
                    await dbContext.SaveChangesAsync();
                }
            }

            if (!dbContext.Roles.Any())
            {
                var roles = GetRole();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }

        private IEnumerable<IdentityRole> GetRole()
        {
            List<IdentityRole> roles =
                [
                   new (UserRoles.Admin)
                   {
                       NormalizedName = UserRoles.Admin.ToUpper(),
                   },
                   new (UserRoles.Owner)
                   {
                       NormalizedName = UserRoles.Owner.ToUpper(),
                   },
                   new (UserRoles.User)
                   {
                       NormalizedName = UserRoles.User.ToUpper(),
                   }
                ];

            return roles;
        }

        private ICollection<Restaurant> GetRestaurants()
        {
            User owner = new User()
            {
                Email = "seed-user@test.com"
            };

            List<Restaurant> restaurants = [
                new()
                {
                    Owner = owner,
                    Name = "KFC",
                    Category = "Fastfood",
                    Description = "American retaurant",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes =[
                        new()
                        {
                            Name = "Bucket chicken",
                            Description = "Hot chicken",
                            Price = 10.20M,
                        },
                        new()
                        {
                            Name = "Chicken burger",
                            Description = "Spicy",
                            Price = 6.20M
                        },
                        ],
                    Address = new()
                    {
                        City = "Milan",
                        Street = "Via Torino",
                        PostalCode = "20154"
                    }
                },
                new Restaurant()
                {
                    Owner = owner,
                    Name = "BurgerKing",
                    Category = "Fastfood",
                    Description = "American retaurant",
                    ContactEmail = "contact@burgerKing.com",
                    HasDelivery = true,
                    Address = new()
                    {
                        City = "Milan",
                        Street = "Viale Duomo",
                        PostalCode = "20364"
                    }
                }

            ];
            return restaurants;
        }
    }
}
