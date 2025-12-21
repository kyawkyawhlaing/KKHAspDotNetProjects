using Application.Abstractions.Messaging;

namespace MyProject.Application.Users.GetById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
