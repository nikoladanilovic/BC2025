using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;

namespace BootcampApp.Repository.Common
{
    public interface IStaffRepository
    {
        Task<List<StaffModel>> GetAllAsync();
        Task<StaffModel> GetByIdAsync(Guid id);
        Task AddAsync(StaffModel staff);
        Task UpdateAsync(StaffModel staff);
        Task DeleteAsync(Guid id);
    }
}
