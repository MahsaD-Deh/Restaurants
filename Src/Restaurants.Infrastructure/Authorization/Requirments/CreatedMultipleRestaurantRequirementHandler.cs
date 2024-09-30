using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirments
{
    public class CreatedMultipleRestaurantRequirementHandler(IRestaurantsRepository restaurantsRepository,
        IUserContext userContext) : AuthorizationHandler<CreatedMultipleRestaurantRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();

            var restaurants = await restaurantsRepository.GetAllAsync();

            var userRestaurantCreated = restaurants.Count(r => r.OwnerId == currentUser!.id);

            if(userRestaurantCreated >= requirement.MinRestaurantCreated)
            {
               context.Succeed(requirement);
            }
            else
            {
               context.Fail();
            }
                    
        }
    }
}
