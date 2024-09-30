using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries.Dish
{
    public class GetDishesForRestaurantQuery(int restaurantId) : IRequest<IEnumerable<DishDto>>
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
