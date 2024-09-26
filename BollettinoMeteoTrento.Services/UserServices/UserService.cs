#region

using BollettinoMeteoTrento.Domain;

#endregion
namespace BollettinoMeteoTrento.Services.UserServices;

public sealed class UserService
{
    // TODO: This should be replaced by a real database context.
    private readonly List<User> _users = new List<User>();

    public async Task<User> RegisterAsync(User user)
    {
        // TODO: Add logic to hash passwords and check for email uniqueness
        _users.Add(user);
        return await Task.FromResult(user);
    }

    public async Task<User> AuthenticateAsync(string email, string password)
    {
        User? user = _users.SingleOrDefault(u => u.Email == email && u.Password == password);
        return await Task.FromResult(user);
    }
}
