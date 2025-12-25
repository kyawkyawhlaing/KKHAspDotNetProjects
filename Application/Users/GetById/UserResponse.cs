namespace Application.Users.GetById;

public sealed record UserResponse
{
    public Guid Id { get; init; }

    public string DisplayName { get; init; }

    public string RefreshToken { get; init; }

    public DateTime? RefreshTokenExpiry { get; init; }
}
