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
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID provided.");
            }
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id), "ID cannot be null.");
            }

            return await _customerRepository.GetByIdAsync(id);
        }
        public async Task AddAsync(CustomerModel customer)
        {
            if (customer.Id == Guid.Empty)
            {
                customer.Id = Guid.NewGuid(); // Ensure a new ID
            }
            else
            {
                // Optionally, you could check if the ID already exists and handle accordingly
                var existingCustomer = await _customerRepository.GetByIdAsync(customer.Id);
                if (existingCustomer != null)
                {
                    throw new InvalidOperationException("Customer with this ID already exists.");
                }
            }
            if (string.IsNullOrWhiteSpace(customer.Name) || string.IsNullOrWhiteSpace(customer.Phone) || string.IsNullOrWhiteSpace(customer.Email))
            {
                throw new ArgumentException("Invalid customer details provided.");
            }

            await _customerRepository.AddAsync(customer);
        }

        public async Task UpdateAsync(CustomerModel customer)
        {
            if (customer.Id == Guid.Empty)
            {
                customer.Id = Guid.NewGuid(); // Ensure a new ID
            }
            else
            {
                // Optionally, you could check if the ID already exists and handle accordingly
                var existingCustomer = await _customerRepository.GetByIdAsync(customer.Id);
                if (existingCustomer != null)
                {
                    throw new InvalidOperationException("Customer with this ID already exists.");
                }
            }
            if (string.IsNullOrWhiteSpace(customer.Name) || string.IsNullOrWhiteSpace(customer.Phone) || string.IsNullOrWhiteSpace(customer.Email))
            {
                throw new ArgumentException("Invalid customer details provided.");
            }
            await _customerRepository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid ID provided.");
            }
            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null)
            {
                throw new InvalidOperationException("Customer with this ID does not exist.");
            }
            await _customerRepository.DeleteAsync(id);
        }
    }
    
}
