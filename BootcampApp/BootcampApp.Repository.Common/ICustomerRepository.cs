using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;

namespace BootcampApp.Repository.Common
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerModel>> GetAllAsync();
        Task<CustomerModel?> GetByIdAsync(Guid id);
        Task AddAsync(CustomerModel customer);
        Task UpdateAsync(CustomerModel customer);
        Task DeleteAsync(Guid id);
    }
}
