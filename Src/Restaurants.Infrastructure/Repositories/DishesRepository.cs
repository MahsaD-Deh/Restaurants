﻿using Restaurants.Domain.Model;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Repositories
{
    internal class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
    {
        public async Task<int> Create(Dish entity)
        {
            dbContext.Dishes.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(IEnumerable<Dish> entities)
        {
            dbContext.Dishes.RemoveRange(entities);
            await dbContext.SaveChangesAsync();
        }
    }
}
