using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Commands.Restaurant.Create;
using Restaurants.Application.Commands.Restaurant.Delete;
using Restaurants.Application.Commands.Restaurant.Update;
using Restaurants.Application.Queries.Restaurant;
using Restaurants.Domain.Constants;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] GetAllRestaurantsQuery query)
        {
            var restaurants = await mediator.Send(query);
            return Ok(restaurants);
        }



        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.HasNationality)]
        public async Task<IActionResult> GetRestaurantById([FromRoute]int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));

            if (restaurant is null)
            {
                return NotFound($"Restaurant with id {id} does not exist");
            }

            return Ok(restaurant);
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
        {
            command.Id = id;
            var isUpdated = await mediator.Send(command);

            if (isUpdated)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));

            if (isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetRestaurantById), new {id}, null);
        }

        [HttpPost("{id}/logo")]
        public async Task<IActionResult> UploadLogo([FromRoute]int id, IFormFile file)
        {
            using var stream = file.OpenReadStream();

            var command = new UploadRestaurantLogoCommand()
            {
                RestaurantId = id,
                FileName = file.FileName,
                File = stream

            };
            await mediator.Send(command);
            return NoContent();
        }
    }
}
