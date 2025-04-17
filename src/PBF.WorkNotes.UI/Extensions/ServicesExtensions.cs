namespace PBF.WorkNotes.UI.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IToDoStateService, ToDoStateService>();

        return services;
    }
}