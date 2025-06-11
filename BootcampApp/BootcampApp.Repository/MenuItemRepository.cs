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


        public async Task<List<MenuItemModel>> GetMenuItems()
        {

            var dishes = new List<MenuItemModel>();


            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();  //conn.Open(); // Uncomment if you want to use sync

                using (var cmd = new NpgsqlCommand("SELECT * FROM \"MenuItems\"", conn))
                using (var reader = await cmd.ExecuteReaderAsync()) // cmd.ExecuteReader()  // Uncomment if you want to use sync
                {
                    while (await reader.ReadAsync())   // reader.Read(); // Uncomment if you want to use sync
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

        public async Task<bool> AddMenuItem(MenuItemModel menuItem)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = new NpgsqlCommand("INSERT INTO \"MenuItems\" VALUES (uuid_generate_v4(), @name, @price, @categoryId)", connection);
            cmd.Parameters.AddWithValue("name", menuItem.DishName);
            cmd.Parameters.AddWithValue("price", menuItem.PriceOfDish);
            cmd.Parameters.AddWithValue("categoryId", menuItem.CategoryId);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0 ? true : false;
        }

        public async Task<bool> ChangeMenuItem(MenuItemModel menuItem, Guid selectedId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = new NpgsqlCommand("update \"MenuItems\" set \"DishName\" = @name, \"PriceOfDish\" = @price, \"CategoryId\" = @categoryId where \"Id\" = @selectedId", connection);
            cmd.Parameters.AddWithValue("name", menuItem.DishName);
            cmd.Parameters.AddWithValue("price", menuItem.PriceOfDish);
            cmd.Parameters.AddWithValue("categoryId", menuItem.CategoryId);
            cmd.Parameters.AddWithValue("selectedId", selectedId);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0 ? true : false;
        }

        public async Task<bool> RemoveMenuItem(Guid selectedId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = new NpgsqlCommand("delete from \"MenuItems\" where \"Id\" = @selectedId;", connection);
            cmd.Parameters.AddWithValue("selectedId", selectedId);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0 ? true : false;
        }

        public async Task<List<MenuItemModel>> GetMenuItemsCategories(string itemCategory, string orderAscDesc)
        {

            var dishes = new List<MenuItemModel>();


            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();  //conn.Open(); // Uncomment if you want to use sync

                using (var cmd = new NpgsqlCommand("select mi.\"Id\", \"DishName\", \"PriceOfDish\", \"CategoryId\" from \"MenuItems\" mi " +
                    " left join \"MenuCategories\" mc on mi.\"CategoryId\" = mc.\"Id\" where mc.\"Name\" = '" + itemCategory +
                    "' order by mi.\"DishName\" " + orderAscDesc + " limit 5 offset 0;", conn))
                using (var reader = await cmd.ExecuteReaderAsync()) // cmd.ExecuteReader()  // Uncomment if you want to use sync
                {
                    while (await reader.ReadAsync())   // reader.Read(); // Uncomment if you want to use sync
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
    }
}
