using BootcampApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using WebAPI.RESTModels;
using BootcampApp.Model;
using BootcampaApp.Service.Common;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuCategoriesController : Controller
    {
        private IMenuCategoryService _service;

        public MenuCategoriesController(IMenuCategoryService service)
        {
            this._service = service;
        }

        [HttpGet("get-menu-categories")]
        public async Task<IActionResult> GetTheMenuCategories() 
        {
            List<MenuCategoryREST> menuCategoryReturned = new List<MenuCategoryREST>();
            var menuCategories = await _service.GetMenuCategory();
            foreach (var category in menuCategories)
            {
                menuCategoryReturned.Add(new MenuCategoryREST(category.Id, category.Name));
            }
            return Ok(menuCategoryReturned);
        }

        [HttpPost("post-menu-category")]
        public async Task<IActionResult> CreateMenuCategory([FromBody] MenuCategoryModel category)    
        {
            bool isAdded = await _service.AddMenuCategory(category);
            var menuCategories = await _service.GetMenuCategory();
            return isAdded ? Ok(menuCategories) : StatusCode(500, "Insert failed.");
        }


        [HttpPut("change-menu-category-with-id-{selectedId}")]
        public async Task<IActionResult> ChangeMenuCategory(Guid selectedId, [FromBody] MenuCategoryModel category)
        {
            bool isChanged = await _service.ChangeMenuCategory(category, selectedId);
            var menuCategories = await _service.GetMenuCategory();
            return isChanged ? Ok(menuCategories) : StatusCode(500, "Insert failed.");

        }

        [HttpDelete("delete-menu-category-with-id-{selectedId}")]
        public async Task<IActionResult> DeleteMenuCategory(Guid selectedId)
        {
            bool isRemoved = await _service.RemoveMenuCategory(selectedId);
            var menuCategories = await _service.GetMenuCategory();
            return isRemoved ? Ok(menuCategories) : StatusCode(500, "Insert failed.");
        }

    }
}
