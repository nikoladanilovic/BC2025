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
            await _staffRepository.AddAsync(staff);
        }
        public async Task UpdateAsync(StaffModel staff)
        {
            await _staffRepository.UpdateAsync(staff);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _staffRepository.DeleteAsync(id);
        }
        
    }
}
