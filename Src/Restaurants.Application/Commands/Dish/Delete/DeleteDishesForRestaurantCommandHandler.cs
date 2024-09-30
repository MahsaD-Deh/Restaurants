using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Commands.Dish.Delete
{
    public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteDishesForRestaurantCommand, bool>
    {
        public async Task<bool> Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogWarning("Removing all dishes from restaurant : {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantsRepository.GetRestaurantById(request.RestaurantId);
            if (restaurant == null)
            {
                return false;
            }

            if (!restaurantAuthorizationService.Authoriza(restaurant, ResourceOperation.Delete))
            {
                throw new Exception();
            }

            await dishesRepository.Delete(restaurant.Dishes);
            return true;
        }
    }
}
