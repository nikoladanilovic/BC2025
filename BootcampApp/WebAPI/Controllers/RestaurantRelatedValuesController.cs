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
        private IMenuItemService _service;

        public RestaurantRelatedValuesController(IMenuItemService service)
        {
            this._service = service;
        }

        [HttpGet("get-menu-items")]
        public async Task<IActionResult> GetTheMenu()
        {
            //Converting from MenuItemModel to MenuItemREST
            List<MenuItemREST> menuItemsReturned = new List<MenuItemREST>();
            var menuItems = await _service.GetMenuItems();
            foreach (var item in menuItems) {
                menuItemsReturned.Add(new MenuItemREST(item.Id, item.DishName, item.PriceOfDish, item.CategoryId, item.Category));
            }
            return Ok(menuItemsReturned);
        }

        [HttpPost("post-menu-item")]     
        public async Task<IActionResult> CreateMenuItem([FromBody] MenuItemModel menuItem)
        {
            bool isAdded = await _service.AddMenuItem(menuItem);
            var menuItems = await _service.GetMenuItems();
            return isAdded ? Ok(menuItems) : StatusCode(500, "Insert failed.");
        }

        
        [HttpPut("change-menu-item-with-id-{selectedId}")]
        public async Task<IActionResult> ChangeMenuItem(Guid selectedId, [FromBody] MenuItemModel menuItem)     
        {
            bool isChanged = await _service.ChangeMenuItem(menuItem, selectedId);
            var menuItems = await _service.GetMenuItems();
            return isChanged ? Ok(menuItems) : StatusCode(500, "Insert failed.");

        }

        [HttpDelete("delete-menu-item-with-id-{selectedId}")]   
        public async Task<IActionResult> DeleteMenuItem(Guid selectedId)
        {
            bool isRemoved = await _service.RemoveMenuItem(selectedId);
            var menuItems = await _service.GetMenuItems();
            return isRemoved ? Ok(menuItems) : StatusCode(500, "Insert failed.");
        }

        [HttpGet("get-menu-items-categories")]
        public async Task<IActionResult> GetAllMenuItemCategory(string itemCategory, string orderAscDesc)
        {
            //Converting from MenuItemModel to MenuItemREST
            List<MenuItemREST> menuItemsReturned = new List<MenuItemREST>();
            var menuItems = await _service.GetMenuItemsCategories(itemCategory, orderAscDesc);
            foreach (var item in menuItems)
            {
                menuItemsReturned.Add(new MenuItemREST(item.Id, item.DishName, item.PriceOfDish, item.CategoryId, item.Category));
            }
            return Ok(menuItemsReturned);
        }
    }
}
