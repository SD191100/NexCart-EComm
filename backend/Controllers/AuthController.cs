using Microsoft.AspNetCore.Mvc;
using NexCart.Models;
using NexCart.DTOs.Auth;
using NexCart.Services.Interfaces;
namespace NexCart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequestDto request)
    {
        try
        {

            Console.WriteLine("Entered Try");
            if (request.Role == "Seller")
            {
                _authService.Register(request.Email, request.Password, request.Role, request.FirstName, request.LastName,request.ContactNumber, request.CompanyName, request.GSTNumber);
            }
            else
            {
                _authService.Register(request.Email, request.Password, request.Role, request.FirstName, request.LastName);
            }
            //_authService.Register(request.Email, request.Password, request.Role, request.FirstName, request.LastName);
            return Ok(new { Message = "Registration successful" });
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Entered Catch");
            return BadRequest(new { Message = ex.Message + "error" });
        }
        //return Ok(new { Message = "Hello" });
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var token = _authService.Authenticate(request.Email, request.Password);
            return Ok(new { Token = token });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }
}
