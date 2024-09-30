using MediatR;

namespace Restaurants.Application.Commands.Dish.Delete
{
    public class DeleteDishesForRestaurantCommand(int restaurantId) : IRequest<bool>
    {
        public int RestaurantId { get; } = restaurantId;
    }
}
