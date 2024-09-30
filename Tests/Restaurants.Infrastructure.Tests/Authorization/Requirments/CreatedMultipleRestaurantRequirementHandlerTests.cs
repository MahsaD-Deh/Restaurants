using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Model;
using Restaurants.Domain.Repositories;
using FluentAssertions;
using Xunit;

namespace Restaurants.Infrastructure.Authorization.Requirments.Tests
{
    public class CreatedMultipleRestaurantRequirementHandlerTests
    {
        [Fact()]
        public async void HandleRequirementAsyncTest()
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", [], null, null);

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

            var restaurants = new List<Restaurant>()
            {
                new()
                {
                    OwnerId = currentUser.id,
                },
                new()
                {
                    OwnerId = currentUser.id,
                },
                new()
                {
                    OwnerId = "2",
                }
            };

            var restaurantReposityryMock = new Mock<IRestaurantsRepository>();
            restaurantReposityryMock.Setup( r => r.GetAllAsync()).ReturnsAsync(restaurants);

            var requirements = new CreatedMultipleRestaurantRequirement(2);
            var handler = new CreatedMultipleRestaurantRequirementHandler(restaurantReposityryMock.Object,
                userContextMock.Object);

            var context = new AuthorizationHandlerContext([requirements], null, null);




            // act
            await handler.HandleAsync(context);



            // assert
            context.HasSucceeded.Should().BeTrue();


        }
    }
}