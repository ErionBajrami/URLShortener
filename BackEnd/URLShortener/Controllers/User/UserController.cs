using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Database;
using URLShortener.DTOs;
using URLShortener.DTOs.User;
using URLShortener.ModelHelpers;
using URLShortener.Models;
using URLShortener.Service;
using URLShortener.Service.User;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class UserController : Controller
    {
        private UrlShortenerDbContext _context;
        private readonly IUserService _userService;

        public UserController(UrlShortenerDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAllUsers();
            if (users.Any())
            {
                return Ok(users);
            }
            return NotFound("No user exists in the database");
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound($"User with ID {id} not found");
        }

        [HttpGet("{id}/urls")]
        public IActionResult GetUserWithUrls(int id)
        {
            var userWithUrls = _userService.GetUserWithUrls(id);
            if (userWithUrls != null)
            {
                return Ok(userWithUrls);
            }
            return NotFound($"User with ID {id} not found");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel request)
        {
            var user = _context.Users
                .Where(user => user.Email == request.Email)
                .FirstOrDefault(user => user.PasswordHash == request.Password);

            if (user == null)
            {
                return Unauthorized("Email or password did not match");
            }

            var token = TokenService.GenerateToken(user.Id, user.Email, user.PasswordHash);

            if (token == null || token == string.Empty)
            {
                return BadRequest(new { message = "UserName or Password is incorrect" });
            }

            return Ok( token );
       
        }
        
        [HttpPost("signup")]
        public IActionResult Add([FromBody] SignUpModel request)
        { 
            var newUser = _userService.AddUser(request);
            if (newUser != null)
            {
                return Ok(newUser);
            }
            return Conflict("Email is already taken");

        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserUpdate userInput)
        {
            var updatedUser = _userService.UpdateUser(id, userInput);
            if (updatedUser != null)
            {
                return Ok(updatedUser);
            }
            return NotFound($"User with ID {id} not found");
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id) 
        {
            _userService.DeleteUser(id);
            return Ok($"User with ID {id} deleted successfully");
        }
    }
}
