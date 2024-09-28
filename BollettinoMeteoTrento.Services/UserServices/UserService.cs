#region

using BollettinoMeteoTrento.Data;
using BollettinoMeteoTrento.Domain;
using BollettinoMeteoTrento.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

#endregion

namespace BollettinoMeteoTrento.Services.UserServices;

public sealed class UserService
{
    private readonly PostgresContext _context;
    private readonly ILogger<UserService> _logger;
    private readonly IPasswordHasher _passwordHasher;
    private readonly string _secret;

    public UserService(IConfiguration configuration, PostgresContext context, ILogger<UserService> logger, IPasswordHasher passwordHasher)
    {
        _secret = configuration["Jwt:Secret"];
        _context = context;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> RegisterAsync(User user, string password)
    {
        try
        {
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }

            _passwordHasher.CreatePasswordHash(password, _secret, out string passwordHash, out string salt);

            user.HashedPassword = passwordHash;
            user.Salt = salt;

            Data.DTOs.User dtoUser = user.ToDto();

            _context.Users.Add(dtoUser);
            await _context.SaveChangesAsync();

            return user;
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "An error occurred while saving the user.");
            throw new InvalidOperationException("There was a problem registering the user. Please try again.");
        }
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        Data.DTOs.User? dtoUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (dtoUser != null && _passwordHasher.VerifyPasswordHash(password, _secret, dtoUser.Password, dtoUser.Password))
        {
            return dtoUser.ToDomain();
        }
        return null;
    }
}
