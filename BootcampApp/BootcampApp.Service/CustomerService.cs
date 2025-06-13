using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampApp.Model;
using BootcampaApp.Service.Common;
using BootcampApp.Repository.Common;

namespace BootcampApp.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<IEnumerable<CustomerModel>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }
        public async Task<CustomerModel?> GetByIdAsync(Guid id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(CustomerModel customer)
        {
            customer.Id = Guid.NewGuid(); // Ensure a new ID
            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateAsync(CustomerModel customer)
        {
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _customerRepository.DeleteAsync(id);
        }
    }
    
}
