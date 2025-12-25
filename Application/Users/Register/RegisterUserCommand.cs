namespace MyProject.Application.Users.Register;

public sealed record RegisterUserCommand(
    string Email,
    string DisplayName,
    string Password)
    : ICommand<Guid>;
