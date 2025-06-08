using PBF.WorkNotes.Application.UseCases;
using PBF.WorkNotes.Gateways.SQLiteGateway.Helpers;
using AutoMapper.Internal;
namespace PBF.WorkNotes.UI;

public partial class App : System.Windows.Application
{
    //public IServiceProvider? ServiceProvider { get; set; }

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

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AllowNullDestinationValues = true;
            cfg.AllowNullCollections = true;
            cfg.AddProfile<UIMappingProfile>();
            cfg.AddProfile<SQLiteGatewayMappingProfile>();
        });
        services.AddSingleton<IMapper>(new Mapper(config));

        services.AddSQLiteGateway(settingsProvider);
        services.AddServices();
        services.AddViewModels();

        services.AddTransient<IGetAllPrioritiesUseCase, GetAllPrioritiesUseCase>();
        services.AddTransient<IGetAllToDoStatesUseCase, GetAllToDoStatesUseCase>();

        services.AddTransient<IGuidProvider, GuidProvider>();

        services.AddSingleton<MainViewModel>();
        services.AddSingleton<MainWindow>();
    }

    public new static App Current => (App)System.Windows.Application.Current;
}

