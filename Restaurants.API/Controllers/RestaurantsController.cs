using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Resaturants;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await restaurantsService.GetAllRestaurantsAsync();
        return Ok(restaurants);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRestaurantByIdAsync([FromRoute] int id)
    {
        var restaurant = await restaurantsService.GetRestaurantByIdAsync(id);
        if(restaurant is null) return NotFound();
        return Ok(restaurant);
    }
}