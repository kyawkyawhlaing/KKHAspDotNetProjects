namespace NTier.Domain.Features.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<TokenResponseDto>;
