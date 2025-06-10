using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;
using Npgsql;

namespace BootcampApp.Repository
{
    public class MenuItemRepository
    {
        //private static List<MenuItemModel> customers = new List<MenuItemModel>();
        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";


        public List<MenuItemModel> GetMenuItems()
        {

            var dishes = new List<MenuItemModel>();


            using (var conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand("SELECT * FROM \"MenuItems\"", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MenuItemModel newDish = new MenuItemModel();
                        newDish.Id = reader.GetGuid(0);
                        newDish.DishName = reader.GetString(1);
                        newDish.PriceOfDish = reader.GetDouble(2);
                        newDish.CategoryId = reader.GetGuid(3);
                        dishes.Add(newDish);
                    }
                }
            }

            return dishes;
        }

        //public List<Customer> GetAllCustomers() => customers;

        //public void AddCustomer(Customer customer)
        //{
        //    customers.Add(customer);
        //}
    }
}
