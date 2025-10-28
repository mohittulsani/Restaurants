using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
    IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var restaurantId = request.Id;
        logger.LogInformation("Updating restaurant with id: {RestaurantId} using {@Restaurant}", restaurantId, request);
        var restaurant = await restaurantsRepository.GetByIdAsync(restaurantId);
        if (restaurant is null) throw new NotFoundException(nameof(Restaurant), restaurantId.ToString());
        
        mapper.Map(request, restaurant);
        await restaurantsRepository.SaveChanges();
    }
}