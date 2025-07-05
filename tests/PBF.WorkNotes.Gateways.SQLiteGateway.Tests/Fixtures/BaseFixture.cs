namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Fixtures;

[ExcludeFromCodeCoverage]
public class BaseFixture
{
    protected bool FirstFactOrderDone { get; set; } = false;
    protected IServiceProvider ServiceProvider { get; set; } = null!;
    public Guid TestId { get; set; }

    public void Initialize(string dataSourcePrefix)
    {
        if (FirstFactOrderDone == false)
        {
            var settingsProvider = CreateSettingsProvider(GetDataSource(dataSourcePrefix));
            ServiceProvider = CreateServiceProvider(settingsProvider);
            UpdateDatabase(ServiceProvider);
            FirstFactOrderDone = true;
        }
    }

    private ISettingsProvider CreateSettingsProvider(string dataSource)
    {
        return new SettingsProvider
        {
            Settings = new AppSettings
            {
                ConnectionStrings = new List<ConnectionStringSettings>
                {
                    new ConnectionStringSettings
                    {
                        Name = "WorkNotesData",
                        ConnectionString = $"Data Source={dataSource}"
                    }
                }
            }
        };
    }

    private string GetDataSource(string prefix)
    {
        return $"{prefix}-{DateTime.UtcNow.ToString("yyyyMMddHHmmssfff")}.db";
    }

    private IServiceProvider CreateServiceProvider(ISettingsProvider settingsProvider)
    {
        var a = new Migration_2025032201();

        var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == "PBF.WorkNotes.Gateways.SQLiteMigrator");

        var config = new MapperConfiguration(config =>
        {
            config.AllowNullDestinationValues = true;
            config.AllowNullCollections = true;
            config.AddProfile<SQLiteGatewayMappingProfile>();
        },
        new NullLoggerFactory());

        return new ServiceCollection()
            .AddFluentMigratorCore()
            .AddSingleton<IMapper>(new Mapper(config))
            .AddGuidProvider()
            .AddSingleton<AppSettings>(settingsProvider.Settings)
            .AddSQLiteGateway(settingsProvider)
            .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(settingsProvider.GetWorkNotesDataDatabaseConnectionString())
                .ScanIn(assembly).For.Migrations())
                .BuildServiceProvider();
    }

    private void UpdateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }
}
