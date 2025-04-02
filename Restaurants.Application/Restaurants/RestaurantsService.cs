using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> logger) : IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
    {
        logger.LogInformation("Get all restaurants");
        var restaurants = await restaurantsRepository.GetAllRestaurantsAsync();
        var restaurantDtoList = restaurants.Select(RestaurantDto.FromRestaurant);
        return restaurantDtoList!;
    }

    public async Task<RestaurantDto?> GetRestaurantByIdAsync(int id)
    {
        logger.LogInformation("Get restaurant by id");
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(id);
        var restaurantDto = RestaurantDto.FromRestaurant(restaurant);
        return restaurantDto;
    }
}