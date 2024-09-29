#region

using BollettinoMeteoTrento.Data.DTOs;
using BollettinoMeteoTrento.Services.UserServices;
using BollettinoMeteoTrento.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

#endregion

namespace BollettinoMeteoTrento.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(
    UserService userService,
    IJwtUtils jwtUtils) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] User dtoUser)
    {
        Domain.User userDomain = dtoUser.ToDomain();

        Domain.User registeredUser = await userService.RegisterAsync(userDomain);
        string token = jwtUtils.GenerateJwtToken(registeredUser);

        return Ok(new
        {
            Token = token
        });
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> UserLogin([FromBody] User userDto)
    {
        Domain.User? user = await userService.LoginAsync(userDto.Email, userDto.Password);
        if (user is null)
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
}
