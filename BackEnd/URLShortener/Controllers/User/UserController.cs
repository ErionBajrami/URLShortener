using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Database;
using URLShortener.DTOs;
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
                    Urls = user.Urls
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
            var user = _context.Users.FirstOrDefault(user => user.Id == id);
            if(user == null)
            {
                return NotFound("Couldn't find user with the specified id: " + id);
            }
            return Ok(user);
        }

        [HttpPost] 
        public IActionResult AddUser([FromBody] SignUpModel userInput)
        {
            try
            {
                var user = new User
                {
                    Email = userInput.Email,
                    FullName = userInput.FullName,
                    PasswordHash = userInput.Password
                };

                _context.Users.Add(user);
                _context.SaveChanges();
                return Ok("User added successfully");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
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
