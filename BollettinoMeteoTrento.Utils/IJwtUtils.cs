#region

using BollettinoMeteoTrento.Domain;

#endregion
namespace BollettinoMeteoTrento.Utils;

public interface IJwtUtils
{
    string GenerateJwtToken(User user);
}
