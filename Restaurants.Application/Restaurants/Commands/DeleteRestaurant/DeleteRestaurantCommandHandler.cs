using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.Id;
        logger.LogInformation("Deleting restaurant with id: {RestaurantId}", restaurantId);
        var restaurant = await restaurantsRepository.GetByIdAsync(restaurantId);
        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), restaurantId.ToString());
        
        await restaurantsRepository.Delete(restaurant);
    }
}