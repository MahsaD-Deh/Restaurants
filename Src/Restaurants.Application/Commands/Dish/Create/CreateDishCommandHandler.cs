using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;
using Model = Restaurants.Domain.Model;

namespace Restaurants.Application.Commands.Dish.Create
{
    public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IDishesRepository dishesRepository,
        IMapper mapper) : IRequestHandler<CreateDishCommand, bool>
    {
        public async Task<bool> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating new dish : {@DishRequest}", request);

            var restaurant = await restaurantsRepository.GetRestaurantById(request.RestaurantId);

            if (restaurant == null)
            {
                return false;
            }

            var dish = mapper.Map<Model.Dish>(request);

            await dishesRepository.Create(dish);

            return true;
        }
    }
}
