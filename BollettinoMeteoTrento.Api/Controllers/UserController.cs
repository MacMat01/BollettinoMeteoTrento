#region

using BollettinoMeteoTrento.Domain;
using BollettinoMeteoTrento.Services.UserServices;
using BollettinoMeteoTrento.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

#endregion

namespace BollettinoMeteoTrento.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService, IJwtUtils jwtUtils, IMemoryCache memoryCache) : ControllerBase
{

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        User registeredUser = await userService.RegisterAsync(user);
        string token = jwtUtils.GenerateJwtToken(registeredUser);

        memoryCache.Set(registeredUser.Email, registeredUser);

        return Ok(new
        {
            Token = token
        });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> UserLogin([FromBody] User userDto)
    {
        User? user = await userService.AuthenticateAsync(userDto.Email, userDto.Password);
        if (user == null)
        {
            return Unauthorized(new
            {
                message = "Email or password is incorrect"
            });
        }

        string token = jwtUtils.GenerateJwtToken(user);
        return Ok(new
        {
            Token = token
        });
    }

    [HttpGet("GetUser")]
    public IActionResult GetUser([FromQuery] string email)
    {
        if (memoryCache.TryGetValue(email, out User? user))
        {
            return Ok(user);
        }
        return NotFound();
    }
}
