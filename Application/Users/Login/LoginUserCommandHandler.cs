using Application.Abstractions.Authentication;
using Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, UserDto>
{
    public async Task<Result<UserDto>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await userManager.FindByEmailAsync(command.Email);

        if (user is null)
        {
            return Result.Failure<UserDto>(UserErrors.NotFoundByEmail);
        }

        SignInResult result = await signInManager.CheckPasswordSignInAsync(user, command.Password, false);

        if (!result.Succeeded)
        {
            return Result.Failure<UserDto>(UserErrors.InvalidPassword);
        }

        await signInManager.SignInAsync(user, false);

        var userDto = new UserDto
        {
            Id = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email!,
            Token = await tokenProvider.CreateToken(user)
        };

        return userDto;
    }
}
