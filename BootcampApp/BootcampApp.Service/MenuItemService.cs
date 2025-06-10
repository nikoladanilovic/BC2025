using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;
using BootcampApp.Repository;

namespace BootcampApp.Service
{
    public class MenuItemService
    {
        private readonly MenuItemRepository _repository = new MenuItemRepository();

        public List<MenuItemModel> GetMenuItems()
        {
            return _repository.GetMenuItems();
        }

        public bool AddMenuItem(MenuItemModel menuItem)
        {
            return _repository.AddMenuItem(menuItem);
        }

        public async Task<bool> ChangeMenuItem(MenuItemModel menuItem, Guid selectedId)
        {
            return await _repository.ChangeMenuItem(menuItem, selectedId);
        }

        public async Task<bool> RemoveMenuItem(Guid selectedId)
        {
            return await _repository.RemoveMenuItem(selectedId);
        }
    }
}
