using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper,
    IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
{
    public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all restaurants");
        var restaurants = await restaurantsRepository.GetAllRestaurantsAsync();
        var restaurantsList = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        return restaurantsList!;
    }
}