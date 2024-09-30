﻿using MediatR;
using Restaurants.Application.DTOs;

namespace Restaurants.Application.Queries.Dish
{
    public class GetDishByIdForRestaurantQuery(int restaurantId, int dishId) : IRequest<DishDto>
    {
        public int RestaurantId { get; } = restaurantId;

        public int DishId { get; } = dishId;
    }
}
