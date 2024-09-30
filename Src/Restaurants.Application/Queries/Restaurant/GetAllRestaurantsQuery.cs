using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries.Restaurant
{
    public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
    {
        public string? SearchPhrase { get; set; }
    }
}
