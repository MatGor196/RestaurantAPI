using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantSeeder : IRestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect() &&
                !_dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                _dbContext.Restaurants.AddRange(restaurants);
                _dbContext.SaveChanges();
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "KFC (short for Kentucky Fried Chicken) is an american fast food restaurant",
                    ContactEmail = "contact@kfc.com",
                    ContactNumber = "254-132-324",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashwille Hot Chicken",
                            Description = "",
                            Price = 0M
                        },

                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Description = "",
                            Price = 0M
                        }
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },

                new Restaurant()
                {
                    Name = "McDonald",
                    Category = "Fast Food",
                    Description = "McDonald Company is an american fast food restaurant",
                    ContactEmail = "contactUs@mcdonald.com",
                    ContactNumber = "932-123-432",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "McCrispy",
                            Description = "",
                            Price = 0M
                        }
                    },
                    Address = new Address()
                    {
                        City = "Warszawa",
                        Street = "Złota 59",
                        PostalCode = "00-120"
                    }
                }
            };

            return restaurants;
        }
    }
}
