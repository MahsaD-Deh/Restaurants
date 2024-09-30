using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Model;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(ILogger<RestaurantAuthorizationService> logger,
        IUserContext userContext) : IRestaurantAuthorizationService
    {
        public bool Authoriza(Restaurant restaurant, ResourceOperation resourceOperation)
        {
            var user = userContext.GetCurrentUser();

            logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}",
                user.email,
                resourceOperation,
                restaurant.Name);

            if (resourceOperation == ResourceOperation.Read ||
                resourceOperation == ResourceOperation.Create)
            {
                logger.LogInformation("Create/Read operation - successful authorization");
                return true;
            }

            if (resourceOperation == ResourceOperation.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Delete operation - successful authorization");
                return true;
            }

            if (resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update
                && user.id == restaurant.OwnerId)
            {
                logger.LogInformation("Restaurant Owner - successful authorization");
                return true;
            }

            return false;
        }
    }
}
