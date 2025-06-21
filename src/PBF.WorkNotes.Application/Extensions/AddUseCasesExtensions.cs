namespace PBF.WorkNotes.Application.Extensions;

public static class UseCasesExtensions
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddTransient<IGetAllPrioritiesUseCase, GetAllPrioritiesUseCase>();
        services.AddTransient<IGetAllToDoStatesUseCase, GetAllToDoStatesUseCase>();
        services.AddTransient<ICreateToDoUseCase, CreateToDoUseCase>();
        services.AddTransient<IUpdateToDoUseCase, UpdateToDoUseCase>();
        services.AddTransient<IGetToDoUseCase, GetToDoUseCase>();
        services.AddTransient<IGetToDoStateUseCase, GetToDoStateUseCase>();
        services.AddTransient<IGetPriorityUseCase, GetPriorityUseCase>();
        
        return services;
    }
}