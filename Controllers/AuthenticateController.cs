using Microsoft.AspNetCore.Mvc;
using OpenApiProject1.ServiceTokenGen;
using OpenApiProject1.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Validate the user credentials (this is just a dummy validation)
        if (request.Username == "user" && request.Password == "password")
        {
            var token = _tokenService.GenerateToken(request.Username);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}


