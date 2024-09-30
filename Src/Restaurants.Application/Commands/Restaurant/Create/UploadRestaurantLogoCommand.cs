using MediatR;

namespace Restaurants.Application.Commands.Restaurant.Create
{
    public class UploadRestaurantLogoCommand : IRequest<bool>
    {
        public string FileName { get; set; } = default!;

        public int RestaurantId { get; set; }

        public Stream File { get; set; } = default!;
    }
}
