namespace Domain.Users;

public static class UserErrors
{
    public static  Error InvalidUserCreated(Guid userId) => Error.Problem(
        "Users.InvalidUserCreated",
        "The user could not be created due to invalid data.");

    public static Error FailedToUpdateRefreshToken() => Error.Problem(
        "Users.FailedToUpdateRefreshToken",
        "Refresh token could not be updated.");

    public static Error InvalidPassword => Error.Problem(
    "Users.InvalidPassword",
    "The provided password is incorrect.");

    public static Error InvalidRefreshToken() => Error.Failure(
        "Users.InvalidRefreshToken",
        "The specified refresh token does not exist or has expired.");

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
