using NexCart.Models;

public interface IAddressService
{
    Task<Address> GetAddressByIdAsync(int addressId);
    Task<IEnumerable<Address>> GetAddressesByUserIdAsync(int userId);
    Task AddAddressAsync(Address address);
    Task UpdateAddressAsync(Address address);
    Task DeleteAddressAsync(int addressId);
}
