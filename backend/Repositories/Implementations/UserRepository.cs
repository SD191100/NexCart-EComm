using Microsoft.EntityFrameworkCore;
using NexCart.Data;
using NexCart.Models;
using NexCart.Repositories.Interfaces;
using NexCart.DTOs.Users;

namespace NexCart.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly NexCartDBContext _context;

        public UserRepository(NexCartDBContext context)
        {
            _context = context;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public void Update(int id, UserRequestDTO user)
        {
            var existingUser =_context.Users.FirstOrDefault(u => u.UserId == id);
            if (existingUser != null) {
              existingUser.Email = user.email;
              existingUser.FirstName = user.firstName;
              existingUser.LastName = user.lastName;
                //existingUser.ContactNumber = user.contactNumber;
                existingUser.IsActive = user.IsActive;
            }

            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            var user = GetUserById(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}
