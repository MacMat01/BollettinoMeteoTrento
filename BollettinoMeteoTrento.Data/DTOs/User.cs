#region

using System.ComponentModel.DataAnnotations;

#endregion
namespace BollettinoMeteoTrento.Data.DTOs;

public sealed class User
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
