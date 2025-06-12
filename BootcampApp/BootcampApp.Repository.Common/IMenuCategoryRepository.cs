using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;

namespace BootcampApp.Repository
{
    public interface IMenuCategoryRepository
    {
        Task<List<MenuCategoryModel>> GetMenuCategory();
        Task<bool> AddMenuCategory(MenuCategoryModel menuCategory);
        Task<bool> ChangeMenuCategory(MenuCategoryModel menuCategory, Guid selectedId);
        Task<bool> RemoveMenuCategory(Guid selectedId);
    }
}
