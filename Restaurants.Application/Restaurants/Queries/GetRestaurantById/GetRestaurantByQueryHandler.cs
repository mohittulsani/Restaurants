using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById;

public class GetRestaurantByQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,
    IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
{
    public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get restaurant by id {RestaurantId}", request.Id);
        var restaurant = await restaurantsRepository.GetRestaurantByIdAsync(request.Id) 
            ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString());;
        var restaurantDto = mapper.Map<RestaurantDto>(restaurant);
        return restaurantDto;
    }
}