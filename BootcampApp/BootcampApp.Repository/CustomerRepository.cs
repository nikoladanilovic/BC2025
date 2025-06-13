using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;
using BootcampApp.Repository.Common;

namespace BootcampApp.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";

        public async Task<IEnumerable<CustomerModel>> GetAllAsync()
        {
            var customers = new List<CustomerModel>();
            var query = "SELECT \"Id\", \"Name\", \"Phone\", \"Email\" FROM \"Customers\"";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                customers.Add(new CustomerModel
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Phone = reader.GetString(2),
                    Email = reader.GetString(3)
                });
            }

            return customers;
        }

        public async Task<CustomerModel?> GetByIdAsync(Guid id)
        {
            var query = "SELECT \"Id\", \"Name\", \"Phone\", \"Email\" FROM \"Customers\" WHERE \"Id\" = @Id";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Id", id);

            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new CustomerModel
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Phone = reader.GetString(2),
                    Email = reader.GetString(3)
                };
            }

            return null;
        }

        public async Task AddAsync(CustomerModel customer)
        {
            var query = "INSERT INTO \"Customers\" (\"Id\", \"Name\", \"Phone\", \"Email\") VALUES (@Id, @Name, @Phone, @Email)";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Id", customer.Id);
            cmd.Parameters.AddWithValue("Name", customer.Name);
            cmd.Parameters.AddWithValue("Phone", customer.Phone);
            cmd.Parameters.AddWithValue("Email", customer.Email);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(CustomerModel customer)
        {
            var query = "UPDATE \"Customers\" SET \"Name\" = @Name, \"Phone\" = @Phone, \"Email\" = @Email WHERE \"Id\" = @Id";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Id", customer.Id);
            cmd.Parameters.AddWithValue("Name", customer.Name);
            cmd.Parameters.AddWithValue("Phone", customer.Phone);
            cmd.Parameters.AddWithValue("Email", customer.Email);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var query = "DELETE FROM \"Customers\" WHERE \"Id\" = @Id";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Id", id);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
