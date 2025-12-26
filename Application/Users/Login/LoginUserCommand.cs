using Domain.Users;

namespace Application.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<UserDto>;
