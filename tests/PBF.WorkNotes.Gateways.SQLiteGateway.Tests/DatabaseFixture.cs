namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests;

public class DatabaseFixture
{
    public IServiceProvider ServiceProvider { get; private set; }
    public ISettingsProvider SettingsProvider { get; private set; }

    public DatabaseFixture()
    {
        SettingsProvider = new SettingsProvider
        {
            Settings = new AppSettings
            {
                ConnectionStrings = new List<ConnectionStringSettings>
                {
                    new ConnectionStringSettings
                    {
                        Name = "WorkNotesData",
                        ConnectionString = "Data Source=:memory:"
                    }
                }
            }
        };
        
        //var a = new Migration_2025032201();

        //var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == "PBF.WorkNotes.Gateways.SQLiteMigrator");

        //ServiceProvider = new ServiceCollection()
        //    .AddFluentMigratorCore()
        //    .AddAutoMapperProfiles()
        //    .ConfigureRunner(rb => rb
        //        .AddSQLite()
        //        .WithGlobalConnectionString(SettingsProvider.GetWorkNotesDataDatabaseConnectionString())
        //        .ScanIn(assembly).For.Migrations())
        //        .BuildServiceProvider();
    }
}
