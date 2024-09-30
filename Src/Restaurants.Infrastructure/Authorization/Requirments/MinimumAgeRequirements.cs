using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirments
{
    public class MinimumAgeRequirements(int minAge) : IAuthorizationRequirement
    {
        public int MinAge { get;} = minAge;
    }
}
