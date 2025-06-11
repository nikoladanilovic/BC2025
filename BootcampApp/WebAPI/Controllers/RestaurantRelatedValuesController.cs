using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Runtime.CompilerServices;
using BootcampApp.Service;
using BootcampApp.Model;
using WebAPI.RESTModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantRelatedValuesController : ControllerBase
    {
        private MenuItemService service = new MenuItemService();
        private static List<RestaurantOrder> listOfAvailableDishes = new List<RestaurantOrder>();
        private static List<MenuCategory> listOfMenuCategories = new List<MenuCategory>();

        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";

        private readonly DataAccess _dataAccess = new DataAccess();

        [HttpGet("get-menu-items")]
        public async Task<IActionResult> GetTheMenu()
        {
            //Converting from MenuItemModel to MenuItemREST
            List<MenuItemREST> menuItemsReturned = new List<MenuItemREST>();
            var menuItems = await service.GetMenuItems();
            foreach (var item in menuItems) {
                menuItemsReturned.Add(new MenuItemREST(item.Id, item.DishName, item.PriceOfDish, item.CategoryId, item.Category));
            }
            return Ok(menuItemsReturned);
        }

        [HttpPost("post-menu-item")]     
        public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemModel menuItem)
        {
            bool isAdded = await service.AddMenuItem(menuItem);
            var menuItems = await service.GetMenuItems();
            return isAdded ? Ok(menuItems) : StatusCode(500, "Insert failed.");
        }

        
        [HttpPut("change-menu-item-with-id-{selectedId}")]
        public async Task<IActionResult> ChangeMenuItem(Guid selectedId, [FromBody] MenuItemModel menuItem)     
        {
            bool isChanged = await service.ChangeMenuItem(menuItem, selectedId);
            var menuItems = service.GetMenuItems();
            return isChanged ? Ok(menuItems) : StatusCode(500, "Insert failed.");

        }

        [HttpDelete("delete-menu-item-with-id-{selectedId}")]   
        public async Task<IActionResult> DeleteMenuItem(Guid selectedId)
        {
            bool isRemoved = await service.RemoveMenuItem(selectedId);
            var menuItems = service.GetMenuItems();
            return isRemoved ? Ok(menuItems) : StatusCode(500, "Insert failed.");
        }

        [HttpGet("get-menu-items-categories")]
        public async Task<IActionResult> GetAllMenuItemCategory(string itemCategory, string orderAscDesc)
        {
            //Converting from MenuItemModel to MenuItemREST
            List<MenuItemREST> menuItemsReturned = new List<MenuItemREST>();
            var menuItems = await service.GetMenuItemsCategories(itemCategory, orderAscDesc);
            foreach (var item in menuItems)
            {
                menuItemsReturned.Add(new MenuItemREST(item.Id, item.DishName, item.PriceOfDish, item.CategoryId, item.Category));
            }
            return Ok(menuItemsReturned);
        }
    }
}
