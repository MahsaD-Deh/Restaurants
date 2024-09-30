using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirments
{
    public class CreatedMultipleRestaurantRequirement(int minRestaurantCreated) : IAuthorizationRequirement
    {
        public int MinRestaurantCreated { get; } = minRestaurantCreated;
    }
}
