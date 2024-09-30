using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Commands.Restaurant.Delete
{
    public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Deleteing the restaurant with id: {RstaurantId}", request.Id);
            var restaurant = await restaurantsRepository.GetRestaurantById(request.Id);
            if (restaurant is null)
            {
                return false;
            }

            if (!restaurantAuthorizationService.Authoriza(restaurant, ResourceOperation.Delete))
            {
                throw new Exception();
            }

            await restaurantsRepository.Delete(restaurant);
            return true;
        }
    }
}
