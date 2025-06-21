namespace PBF.WorkNotes.UI.Extensions;

public static class ViewModelExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddTransient<HomeViewModel>();
        services.AddTransient<ToDoViewModel>();
        services.AddTransient<ToDosViewModel>();

        return services;
    }
}