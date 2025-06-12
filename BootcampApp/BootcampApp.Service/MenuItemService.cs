using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;
using BootcampApp.Repository;

namespace BootcampApp.Service
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _repository;

        public MenuItemService(IMenuItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MenuItemModel>> GetMenuItems()
        {
            return await _repository.GetMenuItems();
        }

        public async Task<bool> AddMenuItem(MenuItemModel menuItem)
        {
            return await _repository.AddMenuItem(menuItem);
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
