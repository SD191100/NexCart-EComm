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

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet] 
    [Authorize(Roles ="Admin")]
    public IActionResult GetAllUsers()
    {
        var user = _userService.GetAllUsers();
        if (user == null) return NotFound(new { Message = "User not found" });

        return Ok(user);
    }

    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = _userService.GetUserById(id);
        if (user == null) return NotFound(new { Message = "User not found" });

        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserRequestDTO user)
    {
        _userService.UpdateUser(id,user);
        return Ok(new { Message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public IActionResult DeleteUser(int id)
    {
        _userService.DeleteUser(id);
        return Ok(new { Message = "User deleted successfully" });
    }
}
