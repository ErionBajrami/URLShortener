using URLShortener.Database;
using URLShortener.DTOs;
using URLShortener.DTOs.User;
using URLShortener.ModelHelpers;

namespace URLShortener.Service.User
{
    public class UserService : IUserService
    {
        private readonly UrlShortenerDbContext _context;

        public UserService(UrlShortenerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<UserUrls> GetAllUsers()
        {
            var usersWithUrls = _context.Users
                .GroupJoin(_context.Urls,
                user => user.Id,
                url => url.UserId,
                (user, urls) => new UserUrls
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    CreatedAt = user.CreatedAt,
                    Urls = urls.ToList()
                })
                .ToList();

            return usersWithUrls;
        }

        public UserUrls? GetUserById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);

            return user != null ? new UserUrls
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                CreatedAt = user.CreatedAt
            } : null;
        }

        public UserUrls GetUserWithUrls(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return null;
            }

            var userUrls = _context.Urls.Where(url => url.UserId == id).ToList();
            return new UserUrls
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                CreatedAt = user.CreatedAt,
                Urls = userUrls
            };
        }

        public string Login(LoginModel req)
        {

            var user = _context.Users
                .Where(user => user.Email == req.Email)
                .FirstOrDefault(user => user.PasswordHash == req.Password);

            if (user == null)
            {
                return null;
            }

            bool isAdmin = user.Email == "admin@admin.com" && user.PasswordHash == "admin";

            return TokenService.GenerateToken(user.Id, user.Email, user.PasswordHash, isAdmin);
        }


        public SignUpModel AddUser(SignUpModel request)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            if (existingUser != null)
            {
                return null; // User with the same email already exists
            }

            var newUser = new Models.User
            {
                Email = request.Email,
                FullName = request.FullName,
                PasswordHash = request.Password,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return new SignUpModel
            {
                Email = newUser.Email,
                FullName = newUser.FullName,
                Password = newUser.PasswordHash
            };
        }

        public UserUpdate UpdateUser(int id, UserUpdate userInput)
        {
            var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userToUpdate == null)
            {
                return null; // User not found
            }

            userToUpdate.Email = userInput.Email;
            userToUpdate.FullName = userInput.FullName;
            _context.SaveChanges();

            return new UserUpdate
            {
                Email = userToUpdate.Email,
                FullName = userToUpdate.FullName,
            };
        }

        public void DeleteUser(int id)
        {
            var userToDelete = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userToDelete != null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }
    }
}
