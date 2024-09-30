using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Model;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization;
using Restaurants.Infrastructure.Authorization.Requirments;
using Restaurants.Infrastructure.Authorization.Services;
using Restaurants.Infrastructure.Configuration;
using Restaurants.Infrastructure.Repositories;
using Restaurants.Infrastructure.Seeders;
using Restaurants.Infrastructure.Storage;

namespace Restaurants.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RestaurantsDb");
            services.AddDbContext<RestaurantsDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
            services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
            services.AddScoped<IDishesRepository, DishesRepository>();
            services.AddScoped<IRestaurantAuthorizationService, RestaurantAuthorizationService>();

            
            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<RestaurantsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<RestaurantsDbContext>();



            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyNames.HasNationality,
                 builder => builder.RequireClaim(AppClaimTypes.Nationality, "German", "Iranian"))
                .AddPolicy(PolicyNames.AtLeast20,
                builder => builder.AddRequirements(new MinimumAgeRequirements(20)))
                .AddPolicy(PolicyNames.CreatedAtLeast20Restaurants,
                builder => builder.AddRequirements(new CreatedMultipleRestaurantRequirement(2)));

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementsHandler>();
            services.AddScoped<IAuthorizationHandler, CreatedMultipleRestaurantRequirementHandler>();
            services.Configure<BlobStorageSettings>(configuration.GetSection("BlobStorage"));
            services.AddScoped<IBlobStorageService,BlobStorageService>();

        }
    }
}
