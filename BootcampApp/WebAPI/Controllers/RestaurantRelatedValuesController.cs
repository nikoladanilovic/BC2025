using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Runtime.CompilerServices;
using BootcampApp.Service;
using BootcampApp.Model;
using WebAPI.RESTModels;
using BootcampaApp.Service.Common;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantRelatedValuesController : ControllerBase
    {
        private IMenuItemService _service;
        private readonly ILogger<RestaurantRelatedValuesController> _logger;

        public RestaurantRelatedValuesController(IMenuItemService service, ILogger<RestaurantRelatedValuesController> logger)
        {
            this._service = service;
            _logger = logger;
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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage);
                _logger.LogError("Invalid model: {Errors}", string.Join("; ", errors));
                return BadRequest(ModelState);
            }

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
