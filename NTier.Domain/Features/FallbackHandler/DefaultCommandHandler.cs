namespace NTier.Domain.Features.FallbackHandler;

// to resolve scrutor DI registration when no command handler is found
internal sealed class DefaultCommandHandler : ICommandHandler<DefaultCommand>
{
    public async Task<Result> Handle(DefaultCommand command, CancellationToken cancellationToken)
    {
        return await Task.FromResult(Result.Success());
    }
}
