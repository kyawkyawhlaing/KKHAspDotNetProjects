namespace Domain.Users;

public static class UserErrors
{
    public static  Error InvalidUserCreated(Guid userId) => Error.Problem(
        "Users.InvalidUserCreated",
        "You can't create a user");

    public static Error FailedToUpdateRefreshToken() => Error.Problem(
        "Users.FailedToUpdateRefreshToken",
        "You can't update the refresh token");

    public static Error TokenNotFound() => Error.Failure(
        "Users.TokenNotFound",
        "Token was not found");

    public static Error NotFound(Guid userId) => Error.NotFound(
        "Users.NotFound",
        $"The user with the Id = '{userId}' was not found");

    public static Error Unauthorized() => Error.Failure(
        "Users.Unauthorized",
        "You are not authorized to perform this action.");

    public static readonly Error NotFoundByEmail = Error.NotFound(
        "Users.NotFoundByEmail",
        "The user with the specified email was not found");

    public static readonly Error EmailNotUnique = Error.Conflict(
        "Users.EmailNotUnique",
        "The provided email is not unique");
}
