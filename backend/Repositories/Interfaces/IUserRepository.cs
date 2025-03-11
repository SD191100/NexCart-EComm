using NexCart.Models;
using NexCart.DTOs.Users;

namespace NexCart.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByEmail(string email);
        void Add(User user);
        User GetUserById(int userId);
        void Update(int id, UserRequestDTO user);
        void Delete(int userId);
    }
}
