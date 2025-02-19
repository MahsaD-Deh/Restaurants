﻿using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Model;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantsDbContext dbContext)
        : IRestaurantsRepository
    {
        public async Task<int> Create(Restaurant entity)
        {
            dbContext.Restaurants.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task Delete(Restaurant entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbContext.Restaurants.ToListAsync();

            return restaurants;
        }

        public async Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase)
        {
            var searchPhraseLower = searchPhrase?.ToLower();
            var restaurants = await dbContext
                .Restaurants
                .Where( r=> searchPhraseLower == null || ( r.Name.ToLower().Contains(searchPhraseLower)
                        || r.Description.ToLower().Contains(searchPhraseLower)))
                .ToListAsync();

            return restaurants;
        }

        public async Task<Restaurant?> GetRestaurantById(int id)
        {
            var restaurant = await dbContext.Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync(c => c.Id == id);

            return restaurant;
        }

        public Task SaveChnages()
            => dbContext.SaveChangesAsync();
    }
}
