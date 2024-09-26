#region

using BollettinoMeteoTrento.Domain;
using Microsoft.Extensions.Caching.Memory;

#endregion

namespace BollettinoMeteoTrento.Services.UserServices;

public sealed class UserService(IMemoryCache memoryCache)
{
    public Task<User> RegisterAsync(User user)
    {
        // TODO: Add logic for password hashing and email uniqueness verification

        if (memoryCache.TryGetValue(user.Email, out User _))
        {
            throw new InvalidOperationException("A user with this email already exists.");
        }

        memoryCache.Set(user.Email, user);

        return Task.FromResult(user);
    }

    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        if (!memoryCache.TryGetValue(email, out User? user))
        {
            return await Task.FromResult<User?>(null);
        }
        if (user != null && user.Password == password)
        {
            return await Task.FromResult(user);
        }
        return await Task.FromResult<User?>(null);
    }

    public User? GetUserByEmail(string email)
    {
        memoryCache.TryGetValue(email, out User? user);
        return user;
    }
}
