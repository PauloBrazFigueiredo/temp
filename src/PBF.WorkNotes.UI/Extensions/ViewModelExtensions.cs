namespace PBF.WorkNotes.UI.Extensions;

public static class ViewModelExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<HomeViewModel>();
        services.AddTransient<ToDoViewModel>();

        return services;
    }
}