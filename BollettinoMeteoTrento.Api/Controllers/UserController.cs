#region

using BollettinoMeteoTrento.Domain;
using BollettinoMeteoTrento.Services.UserServices;
using BollettinoMeteoTrento.Utils;
using Microsoft.AspNetCore.Mvc;

#endregion
namespace BollettinoMeteoTrento.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IJwtUtils _jwtUtils;
    private readonly UserService _userService;

    public UserController(UserService userService, IJwtUtils jwtUtils)
    {
        _userService = userService;
        _jwtUtils = jwtUtils;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        // TODO: Here do the necessary checks such as email uniqueness, password hashing, etc.
        User registeredUser = await _userService.RegisterAsync(user);
        string token = _jwtUtils.GenerateJwtToken(registeredUser);
        return Ok(new
        {
            Token = token
        });
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] User userDto)
    {
        User? user = await _userService.AuthenticateAsync(userDto.Email, userDto.Password);

        string token = _jwtUtils.GenerateJwtToken(user);
        return Ok(new
        {
            Token = token
        });
    }
}
