using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;

namespace BootcampApp.Repository
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItemModel>> GetMenuItems();
        Task<bool> AddMenuItem(MenuItemModel menuItem);
        Task<bool> ChangeMenuItem(MenuItemModel menuItem, Guid selectedId);
        Task<bool> RemoveMenuItem(Guid selectedId);
        Task<List<MenuItemModel>> GetMenuItemsCategories(string itemCategory, string orderAscDesc);
    }
}
