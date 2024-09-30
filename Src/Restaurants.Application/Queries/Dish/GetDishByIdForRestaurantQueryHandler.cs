using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Queries.Dish
{
    public class GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting dish with id : {DishId} for restaurant : {RestaurantId}",
                request.DishId,
                request.RestaurantId);

            var restaurant = await restaurantsRepository.GetRestaurantById(request.RestaurantId);

            if (restaurant == null)
            {
                return null;
            }

            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);

            if (dish == null)
            {
                return null;
            }

            var result = mapper.Map<DishDto>(dish);
            return result;

        }

    }
}
