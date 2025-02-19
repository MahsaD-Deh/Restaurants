﻿using MediatR;

namespace Restaurants.Application.Commands.Dish.Create
{
    public class CreateDishCommand : IRequest<bool>
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int? KiloCalories { get; set; }

        public int RestaurantId { get; set; }
    }
}
