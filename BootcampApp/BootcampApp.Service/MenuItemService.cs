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
            // Example logic: could add filtering, validation, etc.
            return _repository.GetMenuItems();
        }

        //public void CreateCustomer(Customer customer)
        //{
        //    if (string.IsNullOrWhiteSpace(customer.Name))
        //        throw new ArgumentException("Customer name is required.");

        //    _repository.AddCustomer(customer);
        //}
    }
}
