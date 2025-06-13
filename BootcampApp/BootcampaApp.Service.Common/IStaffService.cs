using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;

namespace BootcampaApp.Service.Common
{
    public interface IStaffService
    {
        Task<List<StaffModel>> GetAllAsync();
        Task<StaffModel> GetByIdAsync(Guid id);
        Task AddAsync(StaffModel staff);
        Task UpdateAsync(StaffModel staff);
        Task DeleteAsync(Guid id);
    }
}
