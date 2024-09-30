using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Queries.Restaurant
{
    public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
    {
        public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting the restaurant with id: {RestaurantId}", request.Id);
            var restaurant = await restaurantsRepository.GetRestaurantById(request.Id);
            var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);

            return restaurantDto;
        }
    }
}
