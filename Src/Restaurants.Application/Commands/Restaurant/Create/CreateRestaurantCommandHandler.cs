using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Model = Restaurants.Domain.Model;
using Restaurants.Domain.Repositories;
using Restaurants.Application.Users;

namespace Restaurants.Application.Commands.Restaurant.Create
{
    public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
        IMapper mapper,
        IRestaurantsRepository restaurantsRepository,
        IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("Creating a new restaurant {@Restaurant} with {UserName}", request, currentUser.email);

            var restaurant = mapper.Map<Model.Restaurant>(request);
            restaurant.OwnerId = currentUser.id;
            int id = await restaurantsRepository.Create(restaurant);
            return id;
        }
    }
}
