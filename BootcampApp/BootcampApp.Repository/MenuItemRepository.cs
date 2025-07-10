using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;
using Npgsql;

namespace BootcampApp.Repository
{
    public class MenuItemRepository : IMenuItemRepository
    {
        //private static List<MenuItemModel> customers = new List<MenuItemModel>();
        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";


        public async Task<List<MenuItemModel>> GetMenuItems()
        {

            var dishes = new List<MenuItemModel>();


            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();  //conn.Open(); // Uncomment if you want to use sync

                using (var cmd = new NpgsqlCommand("select mi.\"Id\", \"DishName\", \"PriceOfDish\", \"CategoryId\", \"Name\" from \"MenuItems\" mi" +
                    " left join \"MenuCategories\" mc on mi.\"CategoryId\" = mc.\"Id\"", conn))
                using (var reader = await cmd.ExecuteReaderAsync()) // cmd.ExecuteReader()  // Uncomment if you want to use sync
                {
                    while (await reader.ReadAsync())   // reader.Read(); // Uncomment if you want to use sync
                    {
                        MenuItemModel newDish = new MenuItemModel();
                        MenuCategoryModel newCategory = new MenuCategoryModel();
                        newDish.Id = reader.GetGuid(0);
                        newDish.DishName = reader.GetString(1);
                        newDish.PriceOfDish = reader.GetDouble(2);
                        newDish.CategoryId = reader.GetGuid(3);
                        newCategory.Id = reader.GetGuid(3);
                        newCategory.Name = reader.GetString(4);
                        newDish.Category = newCategory;
                        dishes.Add(newDish);
                    }
                }
            }

            return dishes;
        }

        //public List<Customer> GetAllCustomers() => customers;

        public async Task<bool> AddMenuItem(MenuItemModel menuItem)
        {
            int rowsAffected = 0;
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            if (await isForwardedCategoryIdUnique(menuItem.CategoryId))
            {
                var cmd = new NpgsqlCommand("INSERT INTO \"MenuCategories\" VALUES (@categoryId, @nameOfCategory);" +
                    "INSERT INTO \"MenuItems\" VALUES (uuid_generate_v4(), @name, @price, @categoryId);", connection);
                cmd.Parameters.AddWithValue("name", menuItem.DishName);
                cmd.Parameters.AddWithValue("price", menuItem.PriceOfDish);
                cmd.Parameters.AddWithValue("categoryId", menuItem.CategoryId);
                cmd.Parameters.AddWithValue("nameOfCategory", menuItem.Category.Name);

                rowsAffected = await cmd.ExecuteNonQueryAsync();
            }
            else
            {

                var cmd = new NpgsqlCommand("INSERT INTO \"MenuItems\" VALUES (uuid_generate_v4(), @name, @price, @categoryId);", connection);
                cmd.Parameters.AddWithValue("name", menuItem.DishName);
                cmd.Parameters.AddWithValue("price", menuItem.PriceOfDish);
                cmd.Parameters.AddWithValue("categoryId", menuItem.CategoryId);

                rowsAffected = await cmd.ExecuteNonQueryAsync();

            }

            return rowsAffected > 0 ? true : false;
        }

        public async Task<bool> ChangeMenuItem(MenuItemModel menuItem, Guid selectedId)     
        {
            int rowsAffected = 0;
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            if (await isForwardedCategoryIdUnique(menuItem.CategoryId))
            {
                var cmd = new NpgsqlCommand("INSERT INTO \"MenuCategories\" VALUES (@categoryId, @nameOfCategory); " +
                    "update \"MenuItems\" set \"DishName\" = @name, \"PriceOfDish\" = @price, \"CategoryId\" = @categoryId " +
                    "where \"Id\" = @selectedId; ", connection);
                cmd.Parameters.AddWithValue("name", menuItem.DishName);
                cmd.Parameters.AddWithValue("price", menuItem.PriceOfDish);
                cmd.Parameters.AddWithValue("categoryId", menuItem.CategoryId);
                cmd.Parameters.AddWithValue("nameOfCategory", menuItem.Category.Name);
                cmd.Parameters.AddWithValue("selectedId", selectedId);

                rowsAffected = await cmd.ExecuteNonQueryAsync();
            }
            else
            {
                var cmd = new NpgsqlCommand("update \"MenuItems\" set \"DishName\" = @name, \"PriceOfDish\" = @price, \"CategoryId\" = @categoryId " +
                    "where \"Id\" = @selectedId; ", connection);
                cmd.Parameters.AddWithValue("name", menuItem.DishName);
                cmd.Parameters.AddWithValue("price", menuItem.PriceOfDish);
                cmd.Parameters.AddWithValue("categoryId", menuItem.CategoryId);
                cmd.Parameters.AddWithValue("selectedId", selectedId);

                rowsAffected = await cmd.ExecuteNonQueryAsync();
            }

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

                using (var cmd = new NpgsqlCommand("select * from \"MenuItems\" mi " +
                    " left join \"MenuCategories\" mc on mi.\"CategoryId\" = mc.\"Id\" " +
                    " where mc.\"Name\" = '" + itemCategory +
                    "' order by mi.\"DishName\" " + orderAscDesc + " limit 5 offset 0;", conn))
                using (var reader = await cmd.ExecuteReaderAsync()) // cmd.ExecuteReader()  // Uncomment if you want to use sync
                {
                    while (await reader.ReadAsync())   // reader.Read(); // Uncomment if you want to use sync
                    {
                        MenuItemModel newDish = new MenuItemModel();
                        MenuCategoryModel newCategory = new MenuCategoryModel();
                        newDish.Id = reader.GetGuid(0);
                        newDish.DishName = reader.GetString(1);
                        newDish.PriceOfDish = reader.GetDouble(2);
                        newDish.CategoryId = reader.GetGuid(3);
                        newCategory.Id = reader.GetGuid(4);
                        newCategory.Name = reader.GetString(5);
                        newDish.Category = newCategory;
                        dishes.Add(newDish);
                    }
                }
            }

            return dishes;
        }

        public async Task<bool> isForwardedCategoryIdUnique(Guid categoryId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            var cmd = new NpgsqlCommand("select count(*) from \"MenuCategories\" where \"Id\" = @categoryId", connection);
            cmd.Parameters.AddWithValue("categoryId", categoryId);
            int count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            return count == 0; // If count is 0, the CategoryId is unique
        } 


    }
}
