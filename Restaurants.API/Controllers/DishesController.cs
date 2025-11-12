using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace RestaurantsAPI.Controllers;

[Route("api/restaurant/{restaurantId:int}/dishes")]
[ApiController]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand createDishCommand, 
        IValidator<CreateDishCommand> validator)
    {
        createDishCommand.RestaurantId = restaurantId;
        var result = await validator.ValidateAsync(createDishCommand);
        if (!result.IsValid) return BadRequest(result.Errors);
        
        await mediator.Send(createDishCommand);
        return Created();
    }

    [HttpGet]
    public async Task<IEnumerable<DishDto>> GetAllDishesForRestaurant([FromRoute] int restaurantId)
    {
        var dishes = mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
        return Ok(dishes);
    }
}