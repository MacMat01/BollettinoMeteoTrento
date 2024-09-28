#region

using BollettinoMeteoTrento.Data.DTOs;
using BollettinoMeteoTrento.Services.UserServices;
using BollettinoMeteoTrento.Utils;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace BollettinoMeteoTrento.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService userService, IJwtUtils jwtUtils) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] User dtoUser)
    {
        Domain.User userDomain = dtoUser.ToDomain();

        Domain.User registeredUser = await userService.RegisterAsync(userDomain, dtoUser.Password);
        string token = jwtUtils.GenerateJwtToken(registeredUser);

        return Ok(new
        {
            Token = token
        });
    }

    [HttpPost("Login")]
    public async Task<IActionResult> UserLogin([FromBody] User userDto)
    {
        Domain.User? user = await userService.LoginAsync(userDto.Email, userDto.Password);
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
    public async Task<IActionResult> GetUser([FromQuery] string email)
    {
        Domain.User? user = await userService.LoginAsync(email, string.Empty);
        if (user == null)
        {
            return NotFound();
        }

        User dtoUser = user.ToDto();
        return Ok(dtoUser);
    }
}
