using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Runtime.CompilerServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantRelatedValuesController : ControllerBase
    {
        private static List<RestaurantOrder> listOfAvailableDishes = new List<RestaurantOrder>();
        private static List<MenuCategory> listOfMenuCategories = new List<MenuCategory>();

        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";

        private readonly DataAccess _dataAccess = new DataAccess();

        [HttpGet("get-menu-items")]
        public IActionResult GetTheMenu()
        {
            listOfAvailableDishes = _dataAccess.GetDishes();
            return Ok(listOfAvailableDishes);
        }

        [HttpGet("get-menu-categories")]
        public IActionResult GetTheMenuCategories()
        {
            listOfMenuCategories = _dataAccess.GetMenuCategory();
            return Ok(listOfMenuCategories);
        }


        [HttpPost("post-menu-item")]     
        public async Task<IActionResult> CreateMenuItem([FromBody] RestaurantOrder dish)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = new NpgsqlCommand("INSERT INTO \"MenuItems\" VALUES (uuid_generate_v4(), @name, @price, @categoryId)", connection);
            cmd.Parameters.AddWithValue("name", dish.DishName);
            cmd.Parameters.AddWithValue("price", dish.PriceOfDish);
            cmd.Parameters.AddWithValue("categoryId", dish.CategoryId);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0 ? Ok("Dish added.") : StatusCode(500, "Insert failed.");
        }

        [HttpPost("post-menu-category")]
        public async Task<IActionResult> CreateMenuCategory([FromBody] MenuCategory category)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = new NpgsqlCommand("INSERT INTO \"MenuCategories\" VALUES (uuid_generate_v4(), @name)", connection);
            cmd.Parameters.AddWithValue("name", category.Name);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0 ? Ok("Category added.") : StatusCode(500, "Insert failed.");
        }



        [HttpPut("{selectedName}")]     //TO DO: POPRAVI DA FUNKCIONIRA S NOVE DVIJE TABLICE - MenuItems i MenuCategories
        public async Task<IActionResult> ChangeDish(string selectedName, [FromBody] RestaurantOrder dish)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = new NpgsqlCommand("update \"Menu\" set \"DishName\" = @name, \"PriceOfDish\" = @price where \"DishName\" = @selectedName", connection);
            cmd.Parameters.AddWithValue("name", dish.DishName);
            cmd.Parameters.AddWithValue("price", dish.PriceOfDish);
            cmd.Parameters.AddWithValue("selectedName", selectedName);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0 ? Ok("Dish changed.") : StatusCode(500, "Insert failed.");

        }

        [HttpDelete("{selectedId}")]        //TO DO: POPRAVI DA FUNKCIONIRA S NOVE DVIJE TABLICE - MenuItems i MenuCategories
        public async Task<IActionResult> Delete(Guid selectedId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = new NpgsqlCommand("delete from \"Menu\" where \"id\" = @selectedId;", connection);
            cmd.Parameters.AddWithValue("selectedId", selectedId);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0 ? Ok("Dish changed.") : StatusCode(500, "Insert failed.");

        }

        /*
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
        */
    }
}
