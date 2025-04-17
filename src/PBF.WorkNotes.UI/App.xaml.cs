namespace PBF.WorkNotes.UI;

public partial class App : System.Windows.Application
{
    private IServiceProvider _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        _serviceProvider = serviceCollection.BuildServiceProvider();
        //var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        var mainWindow = _serviceProvider.GetRequiredService<TestWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        ISettingsProvider settingsProvider = new SettingsProvider();

        services.AddTransient<ISettingsProvider, SettingsProvider>();
        services.AddAutoMapperProfiles();
        services.AddSQLiteGateway(settingsProvider);
        services.AddServices();
        services.AddViewModels();

        //services.AddTransient<MainWindow>();
        services.AddTransient<TestWindow>();
    }
}

