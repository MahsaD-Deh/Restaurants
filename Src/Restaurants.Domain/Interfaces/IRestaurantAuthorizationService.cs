using Restaurants.Domain.Constants;
using Restaurants.Domain.Model;

namespace Restaurants.Domain.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        bool Authoriza(Restaurant restaurant, ResourceOperation resourceOperation);
    }
}