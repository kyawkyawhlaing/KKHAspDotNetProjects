using Application.Abstractions.Authentication;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    UserManager<User> userManager) : ICommandHandler<LoginUserCommand, User>
{
    public async Task<Result<User>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(command.Email);

        if (user is null)
        {
            return Result.Failure<User>(UserErrors.NotFoundByEmail);
        }

        bool verified = await userManager.CheckPasswordAsync(user, command.Password);

        if (!verified)
        {
            return Result.Failure<User>(UserErrors.NotFoundByEmail);
        }

        return user;
    }
}
