using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;
using BootcampApp.Repository.Common;

namespace BootcampApp.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";

        public async Task<bool> ExistsAsync(Guid id)
        {
            var query = "SELECT COUNT(*) FROM \"admin\" WHERE \"id\" = @Id";
            await using var conn = new Npgsql.NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new Npgsql.NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Id", id);
            var count = (long)await cmd.ExecuteScalarAsync();
            return count > 0;
        }

        public async Task<IEnumerable<AdminModel>> GetAllAsync()
        {
            var admins = new List<AdminModel>();
            var query = "SELECT \"id\", \"username\", \"email\", \"password\", \"role\" FROM \"admin\"";
            await using var conn = new Npgsql.NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new Npgsql.NpgsqlCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                admins.Add(new AdminModel
                {
                    Id = reader.GetGuid(0),
                    Username = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3),
                    Role = reader.GetString(4)
                });
            }
            return admins;
        }

        public async Task<AdminModel?> GetByIdAsync(Guid id)
        {
            var query = "SELECT \"id\", \"username\", \"email\", \"password\", \"role\" FROM \"admin\" WHERE \"id\" = @Id";
            await using var conn = new Npgsql.NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new Npgsql.NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Id", id);
            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new AdminModel
                {
                    Id = reader.GetGuid(0),
                    Username = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3),
                    Role = reader.GetString(4)
                };
            }
            return null;
        }

        public async Task<bool> CreateAsync(AdminModel admin)
        {
            var query = "INSERT INTO \"admin\" (\"id\", \"username\", \"email\", \"password\", \"role\") VALUES (@Id, @Username, @Email, @Password, @Role)";
            await using var conn = new Npgsql.NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new Npgsql.NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Id", admin.Id);
            cmd.Parameters.AddWithValue("Username", admin.Username);
            cmd.Parameters.AddWithValue("Email", admin.Email);
            cmd.Parameters.AddWithValue("Password", admin.Password);
            cmd.Parameters.AddWithValue("Role", admin.Role);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UpdateAsync(AdminModel admin)
        {
            var query = "UPDATE \"admin\" SET \"username\" = @Username, \"email\" = @Email, \"password\" = @Password, \"role\" = @Role WHERE \"id\" = @Id";
            await using var conn = new Npgsql.NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new Npgsql.NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Id", admin.Id);
            cmd.Parameters.AddWithValue("Username", admin.Username);
            cmd.Parameters.AddWithValue("Email", admin.Email);
            cmd.Parameters.AddWithValue("Password", admin.Password);
            cmd.Parameters.AddWithValue("Role", admin.Role);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var query = "DELETE FROM \"admin\" WHERE \"id\" = @Id";
            await using var conn = new Npgsql.NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new Npgsql.NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Id", id);
            return await cmd.ExecuteNonQueryAsync() > 0;
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            var query = "SELECT COUNT(*) FROM \"admin\" WHERE \"username\" = @Username";
            await using var conn = new Npgsql.NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new Npgsql.NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Username", username);
            var count = (long)await cmd.ExecuteScalarAsync();
            return count > 0;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var query = "SELECT COUNT(*) FROM \"admin\" WHERE \"email\" = @Email";
            await using var conn = new Npgsql.NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new Npgsql.NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Email", email);
            var count = (long)await cmd.ExecuteScalarAsync();
            return count > 0;
        }

        public async Task<AdminModel?> GetByUsernameAsync(string username)
        {
            var query = "SELECT \"id\", \"username\", \"email\", \"password\", \"role\" FROM \"admin\" WHERE \"username\" = @Username";
            await using var conn = new Npgsql.NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new Npgsql.NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Username", username);
            await using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new AdminModel
                {
                    Id = reader.GetGuid(0),
                    Username = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3),
                    Role = reader.GetString(4)
                };
            }
            return null;
        }
    }
}
