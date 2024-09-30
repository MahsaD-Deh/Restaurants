using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries.Restaurant
{
    public class GetRestaurantByIdQuery : IRequest<RestaurantDto?>
    {
        public GetRestaurantByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; }
    }
}
