using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger, 
    IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Delete restaurant with Id {request.Id}");
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.Id);
        if (restaurant is null) return false;
        
        await restaurantsRepository.DeleteRestaurantByIdAsync(restaurant);
        return true;
    }
}