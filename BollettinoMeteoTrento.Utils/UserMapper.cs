#region

using BollettinoMeteoTrento.Domain;

#endregion
namespace BollettinoMeteoTrento.Utils;

public static class UserMapper
{
    public static User ToDomain(this Data.DTOs.User dtoUser)
    {
        return new User
        {
            Id = dtoUser.Id,
            Username = dtoUser.Username,
            Email = dtoUser.Email,
            Password = dtoUser.Password,
            CreatedAt = dtoUser.CreatedAt
        };
    }

    public static Data.DTOs.User ToDto(this User domainUser)
    {
        return new Data.DTOs.User
        {
            Id = domainUser.Id,
            Username = domainUser.Username,
            Email = domainUser.Email,
            Password = domainUser.Password,
            CreatedAt = domainUser.CreatedAt
        };
    }
}
