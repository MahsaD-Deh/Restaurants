using AutoMapper;
using FluentAssertions;
using Restaurants.Application.DTOs;
using Restaurants.Domain.Model;
using Xunit;

namespace Restaurants.Application.Automapper.Tests
{
    public class RestaurantsProfileTests
    {
        [Fact()]
        public void RestaurantsProfileTest_Restaurant_ToRestaurantDto()
        {
            // arrange

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantsProfile>();
            });

            var mapper = configuration.CreateMapper();

            var restaurant = new Restaurant()
            {
                Id = 1,
                Name = "Restaurant44",
                Description = "Desc44",
                Category = "Catego44",
                HasDelivery = true,
                ContactEmail = "Test44@test.com",
                ContactNumber = "3654125",
                Address = new Address()
                {
                    City = "Modena",
                    Street = "At2",
                    PostalCode = "23-365",
                }
            };

            // act
            var restaurantDto = mapper.Map<RestaurantDto>(restaurant);

            // assert

            restaurantDto.Should().NotBeNull();
            restaurantDto.Id.Should().Be(restaurant.Id);
            restaurantDto.Name.Should().Be(restaurant.Name);
        }
    }
}