using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Commands.Restaurant.Delete;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Commands.Restaurant.Update
{
    public class UpdateRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Updating the restaurant with id {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
            var restaurant = await restaurantsRepository.GetRestaurantById(request.Id);
            if (restaurant is null)
            {
                return false;
            }

            if (!restaurantAuthorizationService.Authoriza(restaurant, ResourceOperation.Update))
            {
                throw new Exception();
            }

            mapper.Map(request, restaurant);
            await restaurantsRepository.SaveChnages();
            return true;
        }
    }
}
