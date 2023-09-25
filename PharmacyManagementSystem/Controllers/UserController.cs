using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Model;
using PharmacyManagementSystem.Repository;
using System.Security.Claims;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]

    public class UserController : ControllerBase
    {
        private IUserInterface _userRepository;
        private IEmailInterface _emailRepository;

        public UserController(IUserInterface userRepository, IEmailInterface emailRepository)
        {
            _userRepository = userRepository;
            _emailRepository = emailRepository;
        }

        [HttpPost("Signup")]
        [AllowAnonymous]
        public ActionResult SignUp(User user)
        {
            var user1 = _userRepository.Authenticate(user.PhoneNo, user.Password);
            if (user1 != null)
            {
                return BadRequest("User already exist.");
            }
            else
            {
                _userRepository.Add(user);
                string Subject = $"Welcome {user.Name} to Pharma Care";
                string Body = "You are successfully signed up.";
                _emailRepository.SendEmail(user.Email, Subject, Body);
                return Ok("User registered successfully.");
            }
        }

        [HttpGet("Get User")]
        public ActionResult<User> GetUser()
        {
            var useridClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(useridClaim.Value);
            var user = _userRepository.GetUser(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("Update User")]
        public IActionResult UpdateUser(User user)
        {
            var useridClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(useridClaim.Value);
            if (userId != user.UserId)
            {
                return BadRequest();
            }

            _userRepository.Update(user);
            return Ok(new { message = "User Updated successfully." });
        }

        [HttpDelete("Delete User")]
        public IActionResult DeleteUser()
        {
            var useridClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            int userId = int.Parse(useridClaim.Value);
            _userRepository.Delete(userId);
            return Ok(new { message = "User deleted successfully." });
        }

    }
}
