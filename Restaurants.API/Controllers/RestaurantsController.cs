using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}"), ActionName("GetRestaurantByIdAsync")]
    public async Task<IActionResult> GetRestaurantByIdAsync([FromRoute] int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
        if(restaurant is null) return NotFound();
        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurantAsync(CreateRestaurantCommand command)
    {
        var restaurantId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetRestaurantByIdAsync), new { id = restaurantId }, null);
    }
}