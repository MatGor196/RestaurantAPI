using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public class RestaurantHandler : IRestaurantHandler
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantHandler> _logger;
        public RestaurantHandler(
            RestaurantDbContext dbContext,
            IMapper mapper,
            ILogger<RestaurantHandler> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public RestaurantDto? GetById(int id)
        {
            var searchedRestaurant = _dbContext.Restaurants
              .Include(r => r.Address)
              .Include(r => r.Dishes)
              .FirstOrDefault(r => r.Id == id);

            if (searchedRestaurant is null)
                return null;

            var searchedRestaurantDto = _mapper.Map<RestaurantDto>(searchedRestaurant);
            return searchedRestaurantDto;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext.Restaurants
                .Include(r => r.Address)
                .Include(r => r.Dishes)
                .ToList();

            var restaurantsDto = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantsDto;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

        public void Delete(int id)
        {
            _logger.LogError($"Restaurant with id: {id} DELETE action invoked");

            var restaurantToDelete = _dbContext.Restaurants
                .FirstOrDefault(r => r.Id == id);

            if (restaurantToDelete is null)
            {
                //return false;
                throw new NotFoundException("Restaurant not found");
            }

            _dbContext.Restaurants.Remove(restaurantToDelete);
            _dbContext.SaveChanges();

            //return true;
        }

        public bool Update(int id, UpdateRestaurantDto dto)
        {
            var restaurantToUpdate = _dbContext.Restaurants.
                FirstOrDefault(r => r.Id == id);

            if (restaurantToUpdate is null)
                return false;

            restaurantToUpdate.Name = dto.Name;
            restaurantToUpdate.Description = dto.Description;
            restaurantToUpdate.HasDelivery = dto.HasDelivery;
            _dbContext.SaveChanges();

            return true;
        }
    }
}
