using BootcampApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebAPI.RESTModels;
using BootcampApp.Model;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuCategoriesController : Controller
    {
        private MenuCategoryService service = new MenuCategoryService();
        private static List<RestaurantOrder> listOfAvailableDishes = new List<RestaurantOrder>();
        private static List<MenuCategory> listOfMenuCategories = new List<MenuCategory>();

        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";

        private readonly DataAccess _dataAccess = new DataAccess();

        [HttpGet("get-menu-categories")]
        public async Task<IActionResult> GetTheMenuCategories() 
        {
            List<MenuCategoryREST> menuCategoryReturned = new List<MenuCategoryREST>();
            var menuCategories = await service.GetMenuCategory();
            foreach (var category in menuCategories)
            {
                menuCategoryReturned.Add(new MenuCategoryREST(category.Id, category.Name));
            }
            return Ok(menuCategoryReturned);
        }

        [HttpPost("post-menu-category")]
        public async Task<IActionResult> CreateMenuCategory([FromBody] MenuCategoryModel category)    
        {
            bool isAdded = await service.AddMenuCategory(category);
            var menuCategories = await service.GetMenuCategory();
            return isAdded ? Ok(menuCategories) : StatusCode(500, "Insert failed.");
        }


        [HttpPut("change-menu-category-with-id-{selectedId}")]
        public async Task<IActionResult> ChangeMenuCategory(Guid selectedId, [FromBody] MenuCategoryModel category)
        {
            bool isChanged = await service.ChangeMenuCategory(category, selectedId);
            var menuCategories = await service.GetMenuCategory();
            return isChanged ? Ok(menuCategories) : StatusCode(500, "Insert failed.");

        }

        [HttpDelete("delete-menu-category-with-id-{selectedId}")]
        public async Task<IActionResult> DeleteMenuCategory(Guid selectedId)
        {
            bool isRemoved = await service.RemoveMenuCategory(selectedId);
            var menuCategories = await service.GetMenuCategory();
            return isRemoved ? Ok(menuCategories) : StatusCode(500, "Insert failed.");
        }

    }
}
