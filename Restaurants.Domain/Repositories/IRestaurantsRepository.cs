using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories;

public interface IRestaurantsRepository
{
    Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
    Task<Restaurant?> GetRestaurantByIdAsync(int id);
    Task<int> Create(Restaurant restaurant);
    Task DeleteRestaurantByIdAsync(Restaurant restaurant);
    Task UpdateRestaurantAsync();
}