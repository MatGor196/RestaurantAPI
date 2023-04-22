using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IRestaurantHandler
    {
        int Create(CreateRestaurantDto dto);
        void Delete(int id);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto? GetById(int id);
        bool Update(int id, UpdateRestaurantDto dto);
    }
}