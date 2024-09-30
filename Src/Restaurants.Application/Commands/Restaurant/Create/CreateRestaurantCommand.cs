using MediatR;

namespace Restaurants.Application.Commands.Restaurant.Create
{
    public class CreateRestaurantCommand : IRequest<int>
    {
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public string? Category { get; set; }

        public bool HasDelivery { get; set; }

        public string? ContactEmail { get; set; }

        public string? ContactNumber { get; set; }

        public string? City { get; set; }

        public string? Street { get; set; }

        public string? PostalCode { get; set; }
    }


}
