using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Commands.Restaurant.Create
{
    public class UploadRestaurantLogoCommandHandler(ILogger<UploadRestaurantLogoCommandHandler> logger,
        IRestaurantsRepository restaurantsRepository,
        IRestaurantAuthorizationService restaurantAuthorizationService,
        IBlobStorageService blobStorageService) : IRequestHandler<UploadRestaurantLogoCommand, bool>
    {
        public async Task<bool> Handle(UploadRestaurantLogoCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Uploading restaurant logo for the restaurant with id {RestaurantId}", request.RestaurantId);
            var restaurant = await restaurantsRepository.GetRestaurantById(request.RestaurantId);
            if (restaurant is null)
            {
                return false;
            }

            if (!restaurantAuthorizationService.Authoriza(restaurant, ResourceOperation.Update))
            {
                throw new Exception();
            }

            var logoUrl = await blobStorageService.UploadToBlobAsync(request.File, request.FileName);

            restaurant.LogoUrl = logoUrl;

            await restaurantsRepository.SaveChnages();
            return true;

        }
    }
}
