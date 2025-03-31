using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Resaturants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger) : IRestaurantsService
{
    public async Task<IEnumerable<Restaurant>> GetAllRestaurantsAsync()
    {
        logger.LogInformation("Get all restaurants");
        var restaurants = await restaurantsRepository.GetAllRestaurantsAsync();
        return restaurants;
    }

    public async Task<Restaurant?> GetRestaurantByIdAsync(int id)
    {
        logger.LogInformation("Get restaurant by id");
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(id);
        return restaurant;
    }
}