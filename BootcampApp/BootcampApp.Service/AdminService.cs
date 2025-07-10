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
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task<IEnumerable<AdminModel>> GetAllAsync()
        {
            return await _adminRepository.GetAllAsync();
        }
        public async Task<AdminModel?> GetByIdAsync(Guid id)
        {
            return await _adminRepository.GetByIdAsync(id);
        }
        public async Task<AdminModel?> GetByUsernameAsync(string username)
        {
            return await _adminRepository.GetByUsernameAsync(username);
        }
        public async Task<bool> CreateAsync(AdminModel admin)
        {
            if (admin == null)
            {
                throw new ArgumentNullException(nameof(admin), "Admin cannot be null");
            }
            if (string.IsNullOrWhiteSpace(admin.Username))
            {
                throw new ArgumentException("Username cannot be null or empty", nameof(admin.Username));
            }
            if (string.IsNullOrWhiteSpace(admin.Email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(admin.Email));
            }
            if (string.IsNullOrWhiteSpace(admin.Password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(admin.Password));
            }
            if (await _adminRepository.UsernameExistsAsync(admin.Username))
            {
                throw new InvalidOperationException($"Username '{admin.Username}' already exists.");
            }
            if (await _adminRepository.EmailExistsAsync(admin.Email))
            {
                throw new InvalidOperationException($"Email '{admin.Email}' already exists.");
            }
            admin.Id = Guid.NewGuid(); // Ensure a new ID is generated for the new admin
            admin.Role = "Admin"; // Default role for new admins, can be changed as needed
            // Optionally, you can hash the password here before saving
            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
            // Ensure the password is hashed before saving

            return await _adminRepository.CreateAsync(admin);
        }
        public async Task<bool> UpdateAsync(AdminModel admin)
        {
            if (admin == null)
            {
                throw new ArgumentNullException(nameof(admin), "Admin cannot be null");
            }
            if (string.IsNullOrWhiteSpace(admin.Username))
            {
                throw new ArgumentException("Username cannot be null or empty", nameof(admin.Username));
            }
            if (string.IsNullOrWhiteSpace(admin.Email))
            {
                throw new ArgumentException("Email cannot be null or empty", nameof(admin.Email));
            }
            if (string.IsNullOrWhiteSpace(admin.Password))
            {
                throw new ArgumentException("Password cannot be null or empty", nameof(admin.Password));
            }
            if (await _adminRepository.UsernameExistsAsync(admin.Username) && 
                (await _adminRepository.GetByIdAsync(admin.Id))?.Username != admin.Username)
            {
                throw new InvalidOperationException($"Username '{admin.Username}' already exists.");
            }
            if (await _adminRepository.EmailExistsAsync(admin.Email) && 
                (await _adminRepository.GetByIdAsync(admin.Id))?.Email != admin.Email)
            {
                throw new InvalidOperationException($"Email '{admin.Email}' already exists.");
            }
            // Optionally, you can hash the password here before saving
            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password);

            return await _adminRepository.UpdateAsync(admin);
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _adminRepository.DeleteAsync(id);
        }
        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _adminRepository.ExistsAsync(id);
        }
        public async Task<bool> UsernameExistsAsync(string username)
        {
            return await _adminRepository.UsernameExistsAsync(username);
        }
        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _adminRepository.EmailExistsAsync(email);
        }

    }
}
