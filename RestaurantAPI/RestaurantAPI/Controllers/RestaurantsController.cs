using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using RestaurantAPI.Services;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantHandler _restaurantHandler;

        public RestaurantsController(IRestaurantHandler restaurantHandler)
        {
            _restaurantHandler = restaurantHandler;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll()
        {
            var restaurantsDto = _restaurantHandler.GetAll();
            return Ok(restaurantsDto);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Restaurant> GetById([FromRoute] int id)
        {
            var searchedRestaurantDto = _restaurantHandler.GetById(id);

            if (searchedRestaurantDto is null)
                return NotFound();

            return Ok(searchedRestaurantDto);
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            var Id = _restaurantHandler.Create(dto);
            return Created($"/api/restaurants/{Id}", null);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantHandler.Delete(id);

            //if (isDeleted)
            //    return NoContent();

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update([FromRoute] int id,
                                   [FromBody] UpdateRestaurantDto dto)
        {
            if(!ModelState.IsValid) // <- niepotrzebne gdy mamy [ApiController]
                return BadRequest(ModelState);

            var wasUpdated = _restaurantHandler.Update(id, dto);

            if(!wasUpdated)
                return NotFound();

            return Ok();
        }
    }
}
