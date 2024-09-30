using Restaurants.Domain.Model;

namespace Restaurants.Domain.Repositories
{
    public interface IDishesRepository
    {
        Task<int> Create(Dish entity);

        Task Delete(IEnumerable<Dish> entities);
    }
}
