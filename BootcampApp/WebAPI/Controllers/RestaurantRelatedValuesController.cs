using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Runtime.CompilerServices;
using BootcampApp.Service;
using BootcampApp.Model;

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
        public IActionResult GetTheMenu()
        {
            var menuItems = service.GetMenuItems();     // Should map/convert from MenuItemModel to MenuItemREST
            return Ok(menuItems);
        }

        [HttpPost("post-menu-item")]     
        public IActionResult CreateMenuItem([FromBody] MenuItemModel menuItem)
        {
            bool isAdded = service.AddMenuItem(menuItem);
            var menuItems = service.GetMenuItems();
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

    }
}
