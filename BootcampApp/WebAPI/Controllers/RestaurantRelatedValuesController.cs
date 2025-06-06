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

        [HttpGet("get-menu")]
        public IEnumerable<RestaurantOrder> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new RestaurantOrder
            {
                Id = Guid.NewGuid().ToString(),
                DishName = DishNames[Random.Shared.Next(DishNames.Length)],
                PriceOfDish = Random.Shared.Next(-20, 55)
            })
            .ToArray();
        }

        // POST api/<RestaurantRelatedValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RestaurantRelatedValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RestaurantRelatedValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
