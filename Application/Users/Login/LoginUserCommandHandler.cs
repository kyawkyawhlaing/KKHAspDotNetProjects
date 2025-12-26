using Application.Abstractions.Authentication;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    UserManager<User> userManager,
    SignInManager<User> signInManager) : ICommandHandler<LoginUserCommand, User>
{
    public async Task<Result<User>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(command.Email);

        if (user is null)
        {
            return Result.Failure<User>(UserErrors.NotFoundByEmail);
        }

        SignInResult result = await signInManager.CheckPasswordSignInAsync(user, command.Password, false);

        if (!result.Succeeded)
        {
            return Result.Failure<User>(UserErrors.Unauthorized());
        }

        await signInManager.SignInAsync(user, false);

        return user;
    }
}
