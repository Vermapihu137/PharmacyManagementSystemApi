using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PharmacyManagementSystem.Model;
using PharmacyManagementSystem.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("_myAllowSpecificOrigins")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserInterface _userRepository;
        private readonly IAdmin _adminRepository;
        private ILogger<AuthenticationController> logger;

        public AuthenticationController(IConfiguration configuration, IUserInterface userRepository, IAdmin adminRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _adminRepository = adminRepository;
        }

        [HttpPost("User/Register")]
        public ActionResult UserRegister(User user)
        {
            var existingUser = _userRepository.GetUser(user.UserId);
            if (existingUser != null)
            {
                return BadRequest("User already exists.");
            }

            _userRepository.Add(user);
            return Ok("User registered successfully.");
        }
        [HttpPost("User/Login")]
        public IActionResult UserLogin(Admin user)
        {
            var existingUser = _userRepository.Authenticate(user.PhoneNo, user.Password);
            if (existingUser == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, existingUser.UserId.ToString()),
                new Claim(ClaimTypes.Role, "User")
            };

            var token = GenerateJwtToken(claims);
            return Ok(new { token });
        }

        [HttpPost("Admin/Login")]
        public IActionResult AdminLogin(Admin admin)
        {
            
            var existingAdmin = _adminRepository.AuthenticateAdmin(admin.PhoneNo, admin.Password);
            if (existingAdmin == null || existingAdmin.Password != admin.Password)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, existingAdmin.AdminId.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token = GenerateJwtToken(claims);
            return Ok(new { token });
        }


        private string GenerateJwtToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}