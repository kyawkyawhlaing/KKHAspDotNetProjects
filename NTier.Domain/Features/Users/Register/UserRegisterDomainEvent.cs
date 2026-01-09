namespace NTier.Domain.Features.Users.Register;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
