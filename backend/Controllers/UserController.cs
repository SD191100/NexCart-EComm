using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexCart.DTOs.Users;
using NexCart.Services.Interfaces;

namespace NexCart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILoggerManager _logger;

    public UserController(IUserService userService, ILoggerManager logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAllUsers()
    {
        _logger.LogInfo("Fetching all users");

        var users = _userService.GetAllUsers();
        if (users == null || !users.Any())
        {
            _logger.LogWarn("No users found");
            return NotFound(new { Message = "No users found" });
        }

        _logger.LogInfo($"Retrieved {users.Count()} users");
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        _logger.LogInfo($"Fetching user with ID: {id}");

        var user = _userService.GetUserById(id);
        if (user == null)
        {
            _logger.LogWarn($"User with ID {id} not found");
            return NotFound(new { Message = "User not found" });
        }

        _logger.LogInfo($"User with ID {id} retrieved successfully");
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserRequestDTO user)
    {
        _logger.LogInfo($"Updating user with ID: {id}");

        _userService.UpdateUser(id, user);
        _logger.LogInfo($"User ID {id} updated successfully");

        return Ok(new { Message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteUser(int id)
    {
        _logger.LogInfo($"Deleting user with ID: {id}");

        _userService.DeleteUser(id);
        _logger.LogInfo($"User ID {id} deleted successfully");

        return Ok(new { Message = "User deleted successfully" });
    }
}
