namespace PBF.WorkNotes.UI.Extensions;

public static class ViewModelExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        services.AddTransient<TestViewModel>();

        return services;
    }
}