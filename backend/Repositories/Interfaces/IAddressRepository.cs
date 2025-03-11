using NexCart.Models;

namespace NexCart.Repositories.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> GetAddressByIdAsync(int addressId);
        Task<IEnumerable<Address>> GetAddressesByUserIdAsync(int userId);
        Task AddAddressAsync(Address address);
        Task UpdateAddressAsync(Address address);
        Task DeleteAddressAsync(int addressId);
    }
}
