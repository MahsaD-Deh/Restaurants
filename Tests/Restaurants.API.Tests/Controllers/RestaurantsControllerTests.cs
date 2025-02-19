﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Restaurants.API.Controllers.Tests
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
                _factory = factory;
        }

        [Fact()]
        public async void GetAllTest()
        {
            // arrange 
            var client = _factory.CreateClient();

            // act
            var result = await client.GetAsync("/api/restaurants");

            // assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}