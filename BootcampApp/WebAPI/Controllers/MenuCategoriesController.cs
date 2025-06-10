using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuCategoriesController : Controller
    {
        private static List<RestaurantOrder> listOfAvailableDishes = new List<RestaurantOrder>();
        private static List<MenuCategory> listOfMenuCategories = new List<MenuCategory>();

        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";

        private readonly DataAccess _dataAccess = new DataAccess();

        [HttpGet("get-menu-categories")]
        public IActionResult GetTheMenuCategories()
        {
            listOfMenuCategories = _dataAccess.GetMenuCategory();
            return Ok(listOfMenuCategories);
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


        [HttpPut("change-menu-category-with-id-{selectedId}")]
        public async Task<IActionResult> ChangeMenuItem(Guid selectedId, [FromBody] MenuCategory category)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = new NpgsqlCommand("update \"MenuCategories\" set \"Name\" = @name where \"Id\" = @selectedId", connection);
            cmd.Parameters.AddWithValue("name", category.Name);
            cmd.Parameters.AddWithValue("selectedId", selectedId);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0 ? Ok("Dish changed.") : StatusCode(500, "Insert failed.");

        }

        [HttpDelete("delete-menu-category-with-id-{selectedId}")]
        public async Task<IActionResult> DeleteMenuCategory(Guid selectedId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = new NpgsqlCommand("delete from \"MenuCategories\" where \"Id\" = @selectedId;", connection);
            cmd.Parameters.AddWithValue("selectedId", selectedId);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0 ? Ok("Dish changed.") : StatusCode(500, "Insert failed.");
        }

    }
}
