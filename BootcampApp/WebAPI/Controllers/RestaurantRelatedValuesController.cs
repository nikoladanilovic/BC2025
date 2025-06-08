using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantRelatedValuesController : ControllerBase
    {
        private static List<RestaurantOrder> listOfAvailableDishes = new List<RestaurantOrder>();

        [HttpGet]
        public IActionResult GetTheMenu()
        {
            //var dish = listOfAvailableDishes.FirstOrDefault(p => p.Id == id);
            if (listOfAvailableDishes == null)
                return Content("There are not any dishes on the menu.");

            return Ok(listOfAvailableDishes);
        }

        [HttpPost]
        public IActionResult CreateDish([FromBody] RestaurantOrder dish)
        {
            dish.Id = listOfAvailableDishes.Count + 1;
            listOfAvailableDishes.Add(dish);
            return CreatedAtAction(nameof(GetDishById), new { id = dish.Id }, dish);
        }

        [HttpGet("{id}")]
        public IActionResult GetDishById(int id)
        {
            var dish = listOfAvailableDishes.FirstOrDefault(p => p.Id == id);
            if (dish == null)
                return Content("There is not any dish with id: " + id);

            return Ok(dish);
        }

        [HttpPut("{id}")]
        public IActionResult ChangeDish(int id, [FromBody] RestaurantOrder dish)
        {
            bool hasDishWithId = listOfAvailableDishes.Any(p => p.Id == id);
            if (!hasDishWithId)
            {
                return Content("There is not any dish with id: " + id);
            }
            var dishToBeChanged = listOfAvailableDishes.FirstOrDefault(p => p.Id == id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            dishToBeChanged.DishName = dish.DishName;
            dishToBeChanged.PriceOfDish = dish.PriceOfDish;
            if (dishToBeChanged == null)
                return NotFound();

            return Ok(dishToBeChanged);
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            bool hasDishWithId = listOfAvailableDishes.Any(p => p.Id == id);
            if (hasDishWithId)
            {
                listOfAvailableDishes.RemoveAll(p => p.Id == id);
                return "Dish with an id: " + id + " has been removed!";
            }
            else
            {
                return "There is no dish with the id value " + id;
            }
            
        }

        [HttpPost("insert-menu")]
        public IActionResult CreateWholeMenu([FromBody] List<RestaurantOrder> dishes)
        {
            var addedNumberOfDishes = dishes.Count;
            var currentNumberOfDishes = listOfAvailableDishes.Count;
            if (addedNumberOfDishes != 0)
            {
                foreach (RestaurantOrder dish in dishes)
                {
                    dish.Id = listOfAvailableDishes.Count + 1;
                    listOfAvailableDishes.Add(dish);
                }
                return Content(addedNumberOfDishes + " dishes were added to the menu.");
            }
            else
            {
                return Content("No dishes were added to the menu.");
            }
        }

    }
}
