using BootcampApp.Model;
using BootcampApp.Repository;
using BootcampaApp.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampApp.Service
{
    public class MenuCategoryService : IMenuCategoryService
    {
        private readonly IMenuCategoryRepository _repository;

        public MenuCategoryService(IMenuCategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MenuCategoryModel>> GetMenuCategory()
        {
            return await _repository.GetMenuCategory();
        }

        public async Task<bool> AddMenuCategory(MenuCategoryModel menuCategory)
        {
            return await _repository.AddMenuCategory(menuCategory);
        }

        public async Task<bool> ChangeMenuCategory(MenuCategoryModel menuCategory, Guid selectedId)
        {
            return await _repository.ChangeMenuCategory(menuCategory, selectedId);
        }

        public async Task<bool> RemoveMenuCategory(Guid selectedId)
        {
            return await _repository.RemoveMenuCategory(selectedId);
        }
    }
}
