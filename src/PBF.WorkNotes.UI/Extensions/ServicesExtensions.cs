namespace PBF.WorkNotes.UI.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IToDoStatesService, ToDoStatesService>();
        services.AddTransient<IToDosService, ToDosService>();
        services.AddTransient<IPrioritiesService, PrioritiesService>();
        return services;
    }
}