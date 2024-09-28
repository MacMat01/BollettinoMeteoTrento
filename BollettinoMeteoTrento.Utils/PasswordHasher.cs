#region

using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

#endregion

namespace BollettinoMeteoTrento.Utils;

public abstract class PasswordHasher : IPasswordHasher
{
    public void CreatePasswordHash(string password, string secret, out string passwordHash, out string salt)
    {
        byte[] saltBytes = new byte[16];
        RandomNumberGenerator.Fill(saltBytes);

        salt = Convert.ToBase64String(saltBytes);

        string combinedPassword = password + secret;
        byte[] hashed = KeyDerivation.Pbkdf2(
            combinedPassword,
            saltBytes,
            KeyDerivationPrf.HMACSHA256,
            100000,
            256 / 8
        );

        passwordHash = Convert.ToBase64String(hashed);
    }

    public bool VerifyPasswordHash(string password, string secret, string storedHash, string storedSalt)
    {
        byte[] saltBytes = Convert.FromBase64String(storedSalt);

        string combinedPassword = password + secret;
        byte[] hashed = KeyDerivation.Pbkdf2(
            combinedPassword,
            saltBytes,
            KeyDerivationPrf.HMACSHA256,
            100000,
            256 / 8
        );

        string computedHash = Convert.ToBase64String(hashed);

        return storedHash == computedHash;
    }
}
