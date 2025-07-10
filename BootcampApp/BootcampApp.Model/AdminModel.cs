using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampApp.Model
{
    public class AdminModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } = "Admin"; // Default role for Admin
        public AdminModel(Guid id, string name, string email, string password)
        {
            Id = id;
            Username = name;
            Password = password;
            Email = email;
        }
        public AdminModel() { } // Parameterless constructor for deserialization
    }
}
