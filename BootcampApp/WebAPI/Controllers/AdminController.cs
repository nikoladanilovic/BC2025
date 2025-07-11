using BCrypt.Net;
using BootcampaApp.Service.Common;
using BootcampApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var admins = await _adminService.GetAllAsync();
            return Ok(admins);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var admin = await _adminService.GetByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var admin = await _adminService.GetByUsernameAsync(username);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdminModel admin)
        {
            var created = await _adminService.CreateAsync(admin);
            if (!created)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating admin");
            }
            return CreatedAtAction(nameof(GetById), new { id = admin.Id }, admin);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AdminModel admin)
        {
            if (id != admin.Id)
            {
                return BadRequest("Admin ID mismatch");
            }
            var exists = await _adminService.ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }
            var updated = await _adminService.UpdateAsync(admin);
            if (!updated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating admin");
            }
            return Ok(GetAll());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var exists = await _adminService.ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }
            var deleted = await _adminService.DeleteAsync(id);
            if (!deleted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting admin");
            }
            return Ok(GetAll());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AdminModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                return BadRequest("Invalid login request");
            }
            var admin = await _adminService.GetByUsernameAsync(loginModel.Username);
            if (admin == null || !BCrypt.Net.BCrypt.Verify(loginModel.Password, admin.Password))
            {
                return Unauthorized("Invalid username or password");
            }
            var token = GenerateJwtToken(admin.Username);
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AdminModel admin)
        {
            if (admin == null || string.IsNullOrEmpty(admin.Username) || string.IsNullOrEmpty(admin.Password) || string.IsNullOrEmpty(admin.Email) || string.IsNullOrEmpty(admin.Role))
            {
                return BadRequest("Invalid registration request");
            }
            var existingAdmin = await _adminService.GetByUsernameAsync(admin.Username);
            if (existingAdmin != null)
            {
                return Conflict("Username already exists");
            }
            var created = await _adminService.CreateAsync(admin);
            if (!created)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error registering admin");
            }
            var token = GenerateJwtToken(admin.Username);
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult ProtectedEndpoint()
        {
            var username = User.Identity?.Name;
            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized("You must be logged in to access this endpoint");
            }
            return Ok($"Hello {username}, you have accessed a protected endpoint!");
        }



        private string GenerateJwtToken(string username)
        {
            var claims = new[] { new Claim(ClaimTypes.Name, username) };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-very-long-secret-key-that-is-at-least-33-bytes"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: claims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
