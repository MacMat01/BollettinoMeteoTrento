#region

using System.ComponentModel.DataAnnotations;

#endregion
namespace BollettinoMeteoTrento.Domain;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required] [MaxLength(50)] [MinLength(3)]
    public string FirstName { get; set; } = string.Empty;

    [Required] [MaxLength(50)] [MinLength(3)]
    public string LastName { get; set; } = string.Empty;

    private string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MinLength(8)]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
