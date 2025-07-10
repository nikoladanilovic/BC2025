using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;
using BootcampApp.Repository.Common;
using BootcampaApp.Service.Common;

namespace BootcampApp.Service
{
    public class StaffService : IStaffService
    {
        private readonly IStaffRepository _staffRepository;
        public StaffService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }
        public async Task<List<StaffModel>> GetAllAsync()
        {
            return await _staffRepository.GetAllAsync();
        }
        public async Task<StaffModel> GetByIdAsync(Guid id)
        {
            return await _staffRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(StaffModel staff)
        {
            if (staff.Id == Guid.Empty)
            {
                staff.Id = Guid.NewGuid(); // Ensure a new ID
            }
            else
            {
                // Optionally, you could check if the ID already exists and handle accordingly
                var existingStaff = await _staffRepository.GetByIdAsync(staff.Id);
                while (existingStaff != null)
                {
                    staff.Id = Guid.NewGuid();
                    existingStaff = await _staffRepository.GetByIdAsync(staff.Id);
                }
                
                
            }
            string hireDate = staff.HireDate.ToString();
            if (string.IsNullOrWhiteSpace(staff.Name) || string.IsNullOrWhiteSpace(staff.Role) || staff.Salary <= 0)
            {
                throw new ArgumentException("Invalid staff details provided.");
            }
            await _staffRepository.AddAsync(staff);
        }
        public async Task UpdateAsync(StaffModel staff)
        {
            if (staff.Id == Guid.Empty)
            {
                staff.Id = Guid.NewGuid(); // Ensure a new ID
            }
            else
            {
                // Optionally, you could check if the ID already exists and handle accordingly
                var existingStaff = await _staffRepository.GetByIdAsync(staff.Id);
                if (existingStaff != null)
                {
                    throw new InvalidOperationException("Staff with this ID already exists.");
                }
            }
            if (string.IsNullOrWhiteSpace(staff.Name) || string.IsNullOrWhiteSpace(staff.Role) || staff.HireDate == default || staff.Salary <= 0)
            {
                throw new ArgumentException("Invalid staff details provided.");
            }
            await _staffRepository.UpdateAsync(staff);
        }
        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid staff ID provided.");
            }
            var existingStaff = await _staffRepository.GetByIdAsync(id);
            if (existingStaff == null)
            {
                throw new InvalidOperationException("Staff with this ID does not exist.");
            }
            await _staffRepository.DeleteAsync(id);
        }
        
    }
}
