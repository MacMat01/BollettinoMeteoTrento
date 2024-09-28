#region

using BollettinoMeteoTrento.Data;
using BollettinoMeteoTrento.Domain;
using BollettinoMeteoTrento.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using InvalidOperationException = System.InvalidOperationException;

#endregion

namespace BollettinoMeteoTrento.Services.UserServices;

public sealed class UserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly PostgresContext _postgresContext;

    public UserService(
        PostgresContext postgresContext, 
        ILogger<UserService> logger, 
        IPasswordHasher<User> passwordHasher)
    {
        _postgresContext = postgresContext;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> RegisterAsync(User user)
    {
        await using IDbContextTransaction transaction = await _postgresContext.Database.BeginTransactionAsync();
        try
        {
            if (await _postgresContext.Users.AnyAsync(u => u.Email == user.Email))
            {
                _logger.LogDebug("A user with this email ({email}) already exists.", user.Email);
                return user;
            }

            string passwordHash = _passwordHasher.HashPassword(user, user.Password);

            Data.DTOs.User dtoUser = user.ToDto();

            dtoUser.Password = passwordHash;
            dtoUser.Id = Guid.NewGuid();
            //dtoUser.CreatedAt = DateTime.Now;

            _postgresContext.Users.Add(dtoUser);
            await _postgresContext.SaveChangesAsync();

        

            await transaction.CommitAsync();
            
            return user;
        }
        catch (DbUpdateException ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "An error occurred while saving the user: " + ex.Message);
            throw new InvalidOperationException("An error occurred while saving the user", ex);
        }
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        Data.DTOs.User? dtoUser = await _postgresContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        
//        dtoUser ??= dtoUser.ToDomain();
        
        if (dtoUser is not null)
        {
            PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(
                dtoUser.ToDomain(), 
                dtoUser.Password, 
                password
            );

            if(result == PasswordVerificationResult.Success)
            {
                return dtoUser.ToDomain();
            }
        }
        return null;
    }
}
