global using NTier.SharedKernel;

namespace NTier.Database.Entites;

public sealed class User : Entity
{
    public Guid Id { get; set;  }

    public string DisplayName { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PasswordHash { get; set; }

    public string PasswordSalt { get; set; } = string.Empty;

    public string RefreshToken { get; set; }

    public DateTime? RefreshTokenExpiryTime { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime CreatedOnUtc { get; set; }

    public DateTime? LastUpdatedOnUtc { get; set; }
}
