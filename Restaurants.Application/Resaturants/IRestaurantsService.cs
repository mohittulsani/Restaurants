using Restaurants.Domain.Entities;

namespace Restaurants.Application.Resaturants;

public interface IRestaurantsService
{
    Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync();
    Task<Restaurant?> GetRestaurantByIdAsync(int id);
}