using NexCart.Models;
using NexCart.Repositories.Interfaces;
using NexCart.Services.Interfaces;
using NexCart.DTOs.Users;

namespace NexCart.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }

        public void RegisterUser(User user)
        {
            _userRepository.Add(user);
        }

        public void UpdateUser(int id, UserRequestDTO user)
        {
            _userRepository.Update(id, user);
        }

        public void DeleteUser(int userId)
        {
            _userRepository.Delete(userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
           return  _userRepository.GetAllUsers();
        }
    }
}
