using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;


namespace BootcampApp.Repository
{
    public class MenuCategoryRepository
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";
        public async Task<List<MenuCategoryModel>> GetMenuCategory()
        {
            var allCategories = new List<MenuCategoryModel>();

            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                using (var cmd = new NpgsqlCommand("SELECT * FROM \"MenuCategories\"", conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        MenuCategoryModel newCategory = new MenuCategoryModel();
                        newCategory.Id = reader.GetGuid(0);
                        newCategory.Name = reader.GetString(1);
                        allCategories.Add(newCategory);
                    }
                }
            }

            return allCategories;
        }


        public async Task<bool> AddMenuCategory(MenuCategoryModel menuCategory)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var cmd = new NpgsqlCommand("INSERT INTO \"MenuCategories\" VALUES (uuid_generate_v4(), @name)", connection);
            cmd.Parameters.AddWithValue("name", menuCategory.Name);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();

            return rowsAffected > 0 ? true : false;
        }

        public async Task<bool> ChangeMenuCategory(MenuCategoryModel menuCategory, Guid selectedId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            var cmd = new NpgsqlCommand("UPDATE \"MenuCategories\" SET \"Name\" = @name WHERE \"Id\" = @selectedId", connection);
            cmd.Parameters.AddWithValue("name", menuCategory.Name);
            cmd.Parameters.AddWithValue("selectedId", selectedId);
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0 ? true : false;
        }

        public async Task<bool> RemoveMenuCategory(Guid selectedId)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            var cmd = new NpgsqlCommand("DELETE FROM \"MenuCategories\" WHERE \"Id\" = @selectedId", connection);
            cmd.Parameters.AddWithValue("selectedId", selectedId);
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0 ? true : false;
        }


    }
}
