using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;

namespace BootcampaApp.Service.Common
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminModel>> GetAllAsync();
        Task<AdminModel?> GetByIdAsync(Guid id);
        Task<AdminModel?> GetByUsernameAsync(string username);
        Task<bool> CreateAsync(AdminModel admin);
        Task<bool> UpdateAsync(AdminModel admin);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> UsernameExistsAsync(string username);
        Task<bool> EmailExistsAsync(string email);
    }
}
