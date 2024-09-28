#region

#endregion

namespace BollettinoMeteoTrento.Utils;

public interface IPasswordHasher
{
    void CreatePasswordHash(string password, string secret, out string passwordHash, out string salt);

    bool VerifyPasswordHash(string password, string secret, string storedHash, string storedSalt);
}
