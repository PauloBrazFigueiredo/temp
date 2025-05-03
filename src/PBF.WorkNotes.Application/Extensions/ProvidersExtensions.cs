namespace PBF.WorkNotes.Application.Extensions;

public static class ProvidersExtensions
{
    public static IServiceCollection AddGuidProvider(this IServiceCollection services)
    {
        services.AddSingleton<IGuidProvider, GuidProvider>();

        return services;
    }
}