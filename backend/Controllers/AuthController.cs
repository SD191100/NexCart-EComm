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
    private readonly ILoggerManager _logger;

    public AuthController(IAuthService authService, ILoggerManager logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequestDto request)
    {
        try
        {
            _logger.LogInfo($"register attempt for user: {request.Email}");
            /*Console.WriteLine("Entered Try");*/
            if (request.Role == "Seller")
            {
                _authService.Register(request.Email, request.Password, request.Role, request.FirstName, request.LastName, request.ContactNumber, request.CompanyName, request.GSTNumber);
            }
            else
            {
                _authService.Register(request.Email, request.Password, request.Role, request.FirstName, request.LastName);
            }
            //_authService.Register(request.Email, request.Password, request.Role, request.FirstName, request.LastName);
            _logger.LogInfo($"User {request.Email} register in successfully.");
            return Ok(new { Message = "Registration successful" });
        }
        catch (InvalidOperationException ex)
        {
            /*Console.WriteLine("Entered Catch");*/
            _logger.LogError($"registration Failed failed: {ex.Message}");
            return BadRequest(new { Message = ex.Message + "error" });
        }
        //return Ok(new { Message = "Hello" });
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto request)
    {
        try
        {

            _logger.LogInfo($"register attempt for user: {request.Email}");
            var token = _authService.Authenticate(request.Email, request.Password);
            
            _logger.LogInfo($"User {request.Email} register in successfully.");
            return Ok(new { Token = token });

        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError($"Login failed: {ex.Message}");
            return Unauthorized(new { Message = ex.Message });
        }
    }
}
