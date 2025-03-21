using Microsoft.AspNetCore.Mvc;
using NexCart.DTOs.Adderss;
using NexCart.Models;
using NexCart.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;
    private readonly ILoggerManager _logger;

    public AddressController(IAddressService addressService, ILoggerManager logger)
    {
        _addressService = addressService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AddressResponseDTO>> GetAddress(int id)
    {
        _logger.LogInfo($"Fetching address with ID: {id}");
        var address = await _addressService.GetAddressByIdAsync(id);
        if (address == null)
        {
            _logger.LogWarn($"Address with ID {id} not found.");
            return NotFound();
        }

        _logger.LogInfo($"Address with ID {id} retrieved successfully.");
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
        _logger.LogInfo($"Fetching addresses for User ID: {userId}");
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

        _logger.LogInfo($"{addresses.Count()} addresses retrieved for User ID: {userId}");
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> AddAddress([FromBody] AddressRequestDTO addressRequest)
    {
        _logger.LogInfo("Adding a new address.");
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
        _logger.LogInfo($"Address added successfully with ID: {address.AddressId}");
        return CreatedAtAction(nameof(GetAddress), new { id = address.AddressId }, address);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressRequestDTO addressRequest)
    {
        _logger.LogInfo($"Updating address with ID: {id}");
        var existingAddress = await _addressService.GetAddressByIdAsync(id);
        if (existingAddress == null)
        {
            _logger.LogWarn($"Address with ID {id} not found for update.");
            return NotFound();
        }

        existingAddress.Street = addressRequest.Street;
        existingAddress.City = addressRequest.City;
        existingAddress.State = addressRequest.State;
        existingAddress.Country = addressRequest.Country;
        existingAddress.PostalCode = addressRequest.PostalCode;
        existingAddress.UserId = addressRequest.UserId;
        existingAddress.SellerId = addressRequest.SellerId;

        await _addressService.UpdateAddressAsync(existingAddress);
        _logger.LogInfo($"Address with ID: {id} updated successfully.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        _logger.LogInfo($"Deleting address with ID: {id}");
        var address = await _addressService.GetAddressByIdAsync(id);
        if (address == null)
        {
            _logger.LogWarn($"Address with ID {id} not found for deletion.");
            return NotFound();
        }

        await _addressService.DeleteAddressAsync(id);
        _logger.LogInfo($"Address with ID: {id} deleted successfully.");
        return NoContent();
    }
}
