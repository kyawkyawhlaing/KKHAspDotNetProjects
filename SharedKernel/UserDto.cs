namespace SharedKernel;

public sealed class UserDto
{
    public required Guid Id { get; set; }

    public required string Email { get; set; }

    public required string DisplayName { get; set; }

    public required string Token { get; set; }
}
