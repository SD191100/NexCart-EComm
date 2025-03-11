using Microsoft.AspNetCore.Mvc;
using NexCart.DTOs.Adderss;
using NexCart.Models;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AddressResponseDTO>> GetAddress(int id)
    {
        var address = await _addressService.GetAddressByIdAsync(id);
        if (address == null) return NotFound();

        return Ok(new AddressResponseDTO
        {
            AddressId = address.AddressId,
            Street = address.Street,
            City = address.City,
            State = address.State,
            Country = address.Country,
            PostalCode = address.PostalCode
        });
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<AddressResponseDTO>>> GetAddressesByUser(int userId)
    {
        var addresses = await _addressService.GetAddressesByUserIdAsync(userId);
        var response = addresses.Select(a => new AddressResponseDTO
        {
            AddressId = a.AddressId,
            Street = a.Street,
            City = a.City,
            State = a.State,
            Country = a.Country,
            PostalCode = a.PostalCode
        });
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddAddress([FromBody] AddressRequestDTO addressRequest)
    {
        var address = new Address
        {
            Street = addressRequest.Street,
            City = addressRequest.City,
            State = addressRequest.State,
            Country = addressRequest.Country,
            PostalCode = addressRequest.PostalCode,
            UserId = addressRequest.UserId,
            SellerId = addressRequest.SellerId
        };
        await _addressService.AddAddressAsync(address);
        return CreatedAtAction(nameof(GetAddress), new { id = address.AddressId }, address);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressRequestDTO addressRequest)
    {
        var existingAddress = await _addressService.GetAddressByIdAsync(id);
        if (existingAddress == null) return NotFound();

        existingAddress.Street = addressRequest.Street;
        existingAddress.City = addressRequest.City;
        existingAddress.State = addressRequest.State;
        existingAddress.Country = addressRequest.Country;
        existingAddress.PostalCode = addressRequest.PostalCode;
        existingAddress.UserId = addressRequest.UserId;
        existingAddress.SellerId = addressRequest.SellerId;

        await _addressService.UpdateAddressAsync(existingAddress);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var address = await _addressService.GetAddressByIdAsync(id);
        if (address == null) return NotFound();

        await _addressService.DeleteAddressAsync(id);
        return NoContent();
    }
}
