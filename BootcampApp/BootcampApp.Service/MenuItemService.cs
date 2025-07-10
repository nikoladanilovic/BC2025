using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;
using BootcampApp.Repository;
using BootcampaApp.Service.Common;
using Microsoft.Extensions.Logging;

namespace BootcampApp.Service
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _repository;
        private readonly IMenuCategoryRepository _categoryRepository;
        private readonly ILogger<MenuItemService> _logger;

        public MenuItemService(IMenuItemRepository repository, IMenuCategoryRepository categoryRepository, ILogger<MenuItemService> logger)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        public async Task<List<MenuItemModel>> GetMenuItems()
        {
            return await _repository.GetMenuItems();
        }

        public async Task<bool> AddMenuItem(MenuItemModel menuItem)
        {
            _logger.LogInformation("Adding menu item: {CategoryName}", menuItem.Category.Name);
            List<MenuCategoryModel> allCategories = await _categoryRepository.GetMenuCategory();
            // Check if the category exists in the database
            if (allCategories.Any(c => c.Name == menuItem.Category.Name))
            {
                // If the category exists, proceed to add the menu item
                menuItem.CategoryId = allCategories.First(c => c.Name == menuItem.Category.Name).Id;
                menuItem.Category.Id = menuItem.CategoryId; // Ensure the category ID is set in the model
                //await _categoryRepository.AddMenuCategory(menuItem.Category);
                return await _repository.AddMenuItem(menuItem);
            }
            else
            {
                if(menuItem.Id == Guid.Empty)
                {
                    menuItem.Id = Guid.NewGuid();
                }
                 // Ensure a new ID for the menu item
                menuItem.CategoryId = Guid.NewGuid(); // Assign a new GUID if the category does not exist
                menuItem.Category.Id = menuItem.CategoryId; // Ensure the category ID is set in the model
                // If the category does not exist, add it to the database
                //await _categoryRepository.AddMenuCategory(menuItem.Category);
                return await _repository.AddMenuItem(menuItem);
            }
            
        }

        public async Task<bool> ChangeMenuItem(MenuItemModel menuItem, Guid selectedId)
        {
            return await _repository.ChangeMenuItem(menuItem, selectedId);
        }

        public async Task<bool> RemoveMenuItem(Guid selectedId)
        {
            return await _repository.RemoveMenuItem(selectedId);
        }

        public async Task<List<MenuItemModel>> GetMenuItemsCategories(string itemCategory, string orderAscDesc)
        {
            return await _repository.GetMenuItemsCategories(itemCategory, orderAscDesc);
        }
    }
}
