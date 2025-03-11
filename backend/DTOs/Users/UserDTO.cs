using System.ComponentModel.DataAnnotations;

namespace NexCart.DTOs.Users
{
    public class UserRequestDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public bool IsActive { get; set; } = true;
    }

   
}
