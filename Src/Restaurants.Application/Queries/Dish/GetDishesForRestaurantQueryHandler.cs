using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Queries.Dish
{
    public class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IMapper mapper) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
    {
        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting dishes for restaurant with id : {RestaurantId}", request.RestaurantId);

            var restaurant = await restaurantsRepository.GetRestaurantById(request.RestaurantId);

            if (restaurant == null)
            {
                return Enumerable.Empty<DishDto>();
            }

            var result = mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
            return result;

        }
    }
}
