using Application.Abstractions.Messaging;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyProject.Application.Users.Register;

namespace Application.Users.Register;

internal sealed class RegisterUserCommandHandler(UserManager<User> context)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            UserName = command.Email,
            DisplayName = command.DisplayName,
            PasswordHash = command.Password
        };

        IdentityResult result = await context.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            return Result.Failure<Guid>(UserErrors.InvalidUserCreated(user.Id));
        }

        await context.AddToRoleAsync(user, RegisterRole.Default);

        user.RaiseDomainEvent(new UserRegisteredDomainEvent(user.Id));

        return user.Id;
    }
}
