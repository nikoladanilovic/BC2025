using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantRelatedValuesController : ControllerBase
    {
        ////GET: api/<RestaurantRelatedValuesController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        ////GET api/<RestaurantRelatedValuesController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        private static List<RestaurantOrder> listOfAvailableDishes = new List<RestaurantOrder>();

        private static readonly string[] DishNames = new[]
        {
            "Spagetti", "Pork", "Lamb", "Crabs", "Shrimps"
        };

        //[HttpGet("get-menu")]
        //public IEnumerable<RestaurantOrder> Get()
        //{
        //    return Enumerable.Range(1, 5).Select(index => new RestaurantOrder
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        DishName = DishNames[Random.Shared.Next(DishNames.Length)],
        //        PriceOfDish = Random.Shared.Next(-20, 55)
        //    })
        //    .ToArray();
        //}

        // POST api/<RestaurantRelatedValuesController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

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
                return NotFound();

            return Ok(dish);
        }

        // PUT api/<RestaurantRelatedValuesController>/5
        [HttpPut("{id}")]
        public IActionResult ChangeDish(int id, [FromBody] RestaurantOrder dish)
        {
            var dishToBeChanged = listOfAvailableDishes.FirstOrDefault(p => p.Id == id);
            dishToBeChanged.DishName = dish.DishName;
            dishToBeChanged.PriceOfDish = dish.PriceOfDish;
            if (dishToBeChanged == null)
                return NotFound();

            return Ok(dishToBeChanged);
        }

        // DELETE api/<RestaurantRelatedValuesController>/5
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

    }
}
