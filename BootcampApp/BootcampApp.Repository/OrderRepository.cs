using BootcampApp.Model;
using BootcampApp.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampApp.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";
        public async Task<IEnumerable<OrderModel>> GetAllAsync()
        {
            var orders = new List<OrderModel>();
            const string query = "SELECT \"Id\", \"CustomerId\", \"OrderDate\", \"StaffId\", \"TableNumber\" FROM \"Orders\"";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(query, conn);
            await using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                orders.Add(new OrderModel
                {
                    Id = reader.GetGuid(0),
                    CustomerId = reader.GetGuid(1),
                    OrderDate = reader.GetDateTime(2),
                    StaffId = reader.GetGuid(3),
                    TableNumber = reader.GetInt32(4)
                });
            }

            return orders;
        }

        public async Task<OrderModel?> GetByIdAsync(Guid id)
        {
            const string query = "SELECT \"Id\", \"CustomerId\", \"OrderDate\", \"StaffId\", \"TableNumber\" FROM \"Orders\" WHERE \"Id\" = @Id";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("Id", id);
            await using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new OrderModel
                {
                    Id = reader.GetGuid(0),
                    CustomerId = reader.GetGuid(1),
                    OrderDate = reader.GetDateTime(2),
                    StaffId = reader.GetGuid(3),
                    TableNumber = reader.GetInt32(4)
                };
            }

            return null;
        }

        public async Task AddAsync(OrderModel order)
        {
            const string query = @"
                INSERT INTO ""Orders"" 
                (""Id"", ""CustomerId"", ""OrderDate"", ""StaffId"", ""TableNumber"") 
                VALUES (@Id, @CustomerId, @OrderDate, @StaffId, @TableNumber)";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(query, conn);

            cmd.Parameters.AddWithValue("Id", order.Id);
            cmd.Parameters.AddWithValue("CustomerId", order.CustomerId);
            cmd.Parameters.AddWithValue("OrderDate", order.OrderDate);
            cmd.Parameters.AddWithValue("StaffId", order.StaffId);
            cmd.Parameters.AddWithValue("TableNumber", order.TableNumber);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(OrderModel order)
        {
            const string query = @"
                UPDATE ""Orders"" SET 
                    ""CustomerId"" = @CustomerId, 
                    ""OrderDate"" = @OrderDate, 
                    ""StaffId"" = @StaffId, 
                    ""TableNumber"" = @TableNumber 
                WHERE ""Id"" = @Id";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(query, conn);

            cmd.Parameters.AddWithValue("Id", order.Id);
            cmd.Parameters.AddWithValue("CustomerId", order.CustomerId);
            cmd.Parameters.AddWithValue("OrderDate", order.OrderDate);
            cmd.Parameters.AddWithValue("StaffId", order.StaffId);
            cmd.Parameters.AddWithValue("TableNumber", order.TableNumber);

            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            const string query = "DELETE FROM \"Orders\" WHERE \"Id\" = @Id";

            await using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = new NpgsqlCommand(query, conn);

            cmd.Parameters.AddWithValue("Id", id);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
