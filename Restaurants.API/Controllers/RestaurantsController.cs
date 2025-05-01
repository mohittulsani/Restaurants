using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants")]
public class RestaurantsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var restaurants = await mediator.Send(new GetAllRestaurantsQuery());
        return Ok(restaurants);
    }

    [HttpGet("{id}"), ActionName("GetRestaurantByIdAsync")]
    public async Task<ActionResult<RestaurantDto?>> GetRestaurantByIdAsync([FromRoute] int id)
    {
        var restaurant = await mediator.Send(new GetRestaurantByIdQuery(id));
        if(restaurant is null) return NotFound();
        return Ok(restaurant);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
    {
        var isDeleted = await mediator.Send(new DeleteRestaurantCommand(id));
        if(isDeleted) return NoContent();
        return NotFound();
    }
    
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
    {
        command.Id = id;
        var isUpdated = await mediator.Send(command);
        if(isUpdated) return NoContent();
        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurantAsync(CreateRestaurantCommand command)
    {
        var restaurantId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetRestaurantByIdAsync), new { id = restaurantId }, null);
    }
}