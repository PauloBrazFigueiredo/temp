namespace PBF.WorkNotes.UI.Extensions;

public static class UseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddSingleton<IGetAllPrioritiesUseCase, GetAllPrioritiesUseCase>();

        return services;
    }
}

