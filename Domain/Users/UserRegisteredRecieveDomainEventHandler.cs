namespace Domain.Users;

internal sealed class UserRegisteredRecieveDomainEventHandler : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        Console.WriteLine("Recieving User Registered Event: " + domainEvent.UserId);
        await Task.CompletedTask;
    }
}
