namespace NTier.Api.Extensions;

public static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
    {
        services.AddSwaggerGen(static o =>
        {
            o.CustomSchemaIds(id => id.FullName!.Replace('+', '-'));
        });

        return services;
    }
}
