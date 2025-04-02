using Restaurants.Application.Resaturants.Dtos;

namespace Restaurants.Application.Resaturants;

public interface IRestaurantsService
{
    Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync();
    Task<RestaurantDto?> GetRestaurantByIdAsync(int id);
}