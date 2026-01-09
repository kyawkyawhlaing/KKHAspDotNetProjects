using NTier.Domain.Abstractions.Authentications;

namespace NTier.Domain.Features.Users.Login;

internal sealed class LoginUserCommandHandler(
    AppDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, TokenResponseDto>
{
    public async Task<Result<TokenResponseDto>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<TokenResponseDto>(UserErrors.NotFoundByEmail);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        { 
            return Result.Failure<TokenResponseDto>(UserErrors.NotFoundByEmail); 
        }

        TokenResponseDto token = await tokenProvider.CreateTokenResponse(user);

        return token;
    }
}
