using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Logout;

internal sealed class LogoutUserCommandHandler(UserManager<User> userManager) : ICommandHandler<LogoutUserCommand, string>
{
    public async Task<Result<string>> Handle(LogoutUserCommand command, CancellationToken cancellationToken)
    {
        int result = await userManager.Users
            .Where(x => x.Id.ToString() == command.UserId)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.RefreshToken, _ => null)
                .SetProperty(x => x.RefreshTokenExpiry, _ => null),
                cancellationToken
            );

        if (result is 0)
        {
            return Result.Failure<string>(UserErrors.FailedToUpdateRefreshToken());
        }

        return Result.Success("Refresh token was updated successfully");
    }
}
