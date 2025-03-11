using NexCart.Data;
using NexCart.Models;
using NexCart.Repositories.Interfaces;

public class AddressRepository : IAddressRepository
{
    private readonly NexCartDBContext _context;

    public AddressRepository(NexCartDBContext context)
    {
        _context = context;
    }

    public async Task<Address> GetAddressByIdAsync(int addressId)
    {
        return await _context.Addresses.FindAsync(addressId);
    }

    public async Task<IEnumerable<Address>> GetAddressesByUserIdAsync(int userId)
    {
        return  _context.Addresses.Where(a => a.UserId == userId).ToList();
    }

    public async Task AddAddressAsync(Address address)
    {
        await _context.Addresses.AddAsync(address);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAddressAsync(Address address)
    {
        _context.Addresses.Update(address);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAddressAsync(int addressId)
    {
        var address = await GetAddressByIdAsync(addressId);
        if (address != null)
        {
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }
    }
}
