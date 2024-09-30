using Restaurants.Domain.Model;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();

        Task<IEnumerable<Restaurant>> GetAllMatchingAsync(string? searchPhrase);

        Task<Restaurant?> GetRestaurantById(int id);

        Task<int> Create(Restaurant entity);

        Task Delete(Restaurant entity);

        Task SaveChnages();
    }
}
