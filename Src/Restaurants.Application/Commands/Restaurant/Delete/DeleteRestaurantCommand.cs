using MediatR;

namespace Restaurants.Application.Commands.Restaurant.Delete
{
    public class DeleteRestaurantCommand(int id) : IRequest<bool>
    {
        public int Id { get; } = id;
    }
}
