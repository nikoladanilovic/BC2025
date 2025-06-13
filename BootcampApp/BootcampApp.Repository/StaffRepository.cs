using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;
using BootcampApp.Repository.Common;

namespace BootcampApp.Repository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=admin1235;Database=postgres";
        public async Task<List<StaffModel>> GetAllAsync()
        {
            var staffList = new List<StaffModel>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var command = new NpgsqlCommand("SELECT * FROM \"Staff\"", connection);

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    staffList.Add(new StaffModel
                    {
                        Id = reader.GetGuid(0),
                        Name = reader.GetString(1),
                        Role = reader.GetString(2),
                        HireDate = reader.GetDateTime(3),
                        Salary = reader.GetDouble(4)
                    });
                }
            }
            return staffList;
        }

        public async Task<StaffModel> GetByIdAsync(Guid id)
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand("SELECT * FROM \"Staff\" WHERE \"Id\" = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new StaffModel
                {
                    Id = reader.GetGuid(0),
                    Name = reader.GetString(1),
                    Role = reader.GetString(2),
                    HireDate = reader.GetDateTime(3),
                    Salary = reader.GetDouble(4)
                };
            }

            return null;
        }

        public async Task AddAsync(StaffModel staff)
        {
            using var connection = new NpgsqlConnection(_connectionString) ;
            
            await connection.OpenAsync();

            var command = new NpgsqlCommand("INSERT INTO \"Staff\" (\"Id\", \"Name\", \"Role\", \"HireDate\", \"Salary\") "+
            "VALUES (@id, @name, @role, @hireDate, @salary)", connection);

            command.Parameters.AddWithValue("@id", staff.Id);
            command.Parameters.AddWithValue("@name", staff.Name);
            command.Parameters.AddWithValue("@role", staff.Role);
            command.Parameters.AddWithValue("@hireDate", staff.HireDate);
            command.Parameters.AddWithValue("@salary", staff.Salary);

            await command.ExecuteNonQueryAsync();
            
                
        }

        public async Task UpdateAsync(StaffModel staff)
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand("UPDATE \"Staff\" " +
                "SET \"Name\" = @name, \"Role\" = @role, \"HireDate\" = @hireDate, \"Salary\" = @salary "+
                "WHERE \"Id\" = @id", connection);

            command.Parameters.AddWithValue("@id", staff.Id);
            command.Parameters.AddWithValue("@name", staff.Name);
            command.Parameters.AddWithValue("@role", staff.Role);
            command.Parameters.AddWithValue("@hireDate", staff.HireDate);
            command.Parameters.AddWithValue("@salary", staff.Salary);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            var command = new NpgsqlCommand("DELETE FROM \"Staff\" WHERE \"Id\" = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}
