using NexCart.DTOs.Users;
using NexCart.Models;

namespace NexCart.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int userId);
        User GetUserByEmail(string email);
        void RegisterUser(User user);
        void UpdateUser(int id, UserRequestDTO user);
        void DeleteUser(int userId);
    }
}
