using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Database;
using URLShortener.DTOs;
using URLShortener.DTOs.User;
using URLShortener.ModelHelpers;
using URLShortener.Models;
using URLShortener.Service;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class UserController : Controller
    {
        private UrlShortenerDbContext _context;
        
        public UserController(UrlShortenerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var allUsers = _context.Users
                .Select(user => new User()
                {
                    Id = user.Id,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt,
                    FullName = user.FullName,
                })
                .ToList();
            if(allUsers.Count > 0)
            {
                return Ok(allUsers);
            }
            return NotFound("No user exists in the database");
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound("Couldn't find user with the specified id: " + id);
            }

            // Fetch the URLs associated with the user
            var userUrls = _context.Urls.Where(url => url.UserId == id).ToList();
            var returnUser = new UserUrls
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                CreatedAt = user.CreatedAt,
                Urls = userUrls
            };
            
            return Ok(returnUser);
        }

        [HttpPost("login")]
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

            return Ok(new Dictionary<string, string>() { { "token", token } });
        }
        
        [HttpPost]
        public IActionResult Add([FromBody] SignUpModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the email is already registered
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return Conflict("Email is already taken");
            }
            
            // Create a new User entity
            var newUser = new Models.User
            {
                Email = request.Email,
                FullName = request.FullName,
                PasswordHash = request.Password,
                CreatedAt = DateTime.UtcNow
            };

            _context.Add(newUser);
            _context.SaveChanges();

            return Ok(newUser);

        }

        //[HttpPost] 
        //public IActionResult AddUser([FromBody] SignUpModel userInput)
        //{
        //    try
        //    {
        //        var user = new User
        //        {
        //            Email = userInput.Email,
        //            FullName = userInput.FullName,
        //            PasswordHash = userInput.Password,
        //            CreatedAt = DateTime.UtcNow
        //        };

        //        _context.Users.Add(user);
        //        _context.SaveChanges();
        //        return Ok("User added successfully");
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserUpdate userInput)
        {
            var userToUpdate = _context.Users.FirstOrDefault(user=>user.Id == id);
            if(userToUpdate == null) {
                return NotFound("Couldn't find user with the specified id: " + id);
            }

            //Update the properties of the user
            userToUpdate.Email = userInput.Email;
            userToUpdate.FullName = userInput.FullName;
            _context.SaveChanges();
            return Ok("User updated successfully");
        }

        [HttpDelete]
        public IActionResult DeleteUser(int id) 
        {
            var userToDelete = _context.Users.FirstOrDefault(user => user.Id == id);

            if(userToDelete == null)
            {
                return NotFound("Couldn't find user with the specified id: " + id);
            }

            _context.Users.Remove(userToDelete);
            _context.SaveChanges();

            return Ok("User with id: " + id + " deleted successfully");
        }
    }
}
