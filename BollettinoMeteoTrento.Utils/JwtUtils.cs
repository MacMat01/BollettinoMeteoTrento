#region

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BollettinoMeteoTrento.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

#endregion
namespace BollettinoMeteoTrento.Utils;

public sealed class JwtUtils(IConfiguration configuration) : IJwtUtils
{
    private readonly string _secret = configuration["Jwt:Secret"]!;

    public string GenerateJwtToken(User user)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_secret);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()!)
            }),
            Expires = DateTime.UtcNow.AddSeconds(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
