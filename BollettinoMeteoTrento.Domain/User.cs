#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace BollettinoMeteoTrento.Domain;

public sealed class User
{
    [Key]
    public Guid? Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    [MinLength(3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
