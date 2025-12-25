using Application.Abstractions.Authentication;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    UserManager<User> userManager,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(command.Email);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        bool verified = await userManager.CheckPasswordAsync(user, command.Password);

        if (!verified)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        string token = await tokenProvider.CreateToken(user);

        return token;
    }
}
