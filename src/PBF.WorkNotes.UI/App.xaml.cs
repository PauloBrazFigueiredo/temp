namespace PBF.WorkNotes.UI;

public partial class App : System.Windows.Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        ISettingsProvider settingsProvider = new SettingsProvider();
        services.AddTransient<ISettingsProvider, SettingsProvider>();

        //var config = new MapperConfiguration(config =>
        //{
        //    config.AllowNullDestinationValues = true;
        //    config.AllowNullCollections = true;
        //    config.AddProfile<UIMappingProfile>();
        //    config.AddProfile<SQLiteGatewayMappingProfile>();
        //}, null);
        //services.AddSingleton<IMapper>(new Mapper(config));
        services.AddLogging(configure =>
        {
            configure.AddConsole();
            configure.AddDebug();
        });
        var tempProvider = services.BuildServiceProvider();
        var loggerFactory = tempProvider.GetRequiredService<ILoggerFactory>();
        services.AddUIAutoMapper(loggerFactory);

        services.AddSQLiteGateway(settingsProvider);
        services.AddUseCases();
        services.AddServices();
        services.AddViewModels();

        services.AddTransient<IGuidProvider, GuidProvider>();

        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();
    }

    public new static App Current => (App)System.Windows.Application.Current;
}

