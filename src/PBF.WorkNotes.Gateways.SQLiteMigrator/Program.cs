ISettingsProvider settingsProvider = new SettingsProvider();

using (var serviceProvider = CreateServices(settingsProvider))
using (var scope = serviceProvider.CreateScope())
{
    // Put the database update into a scope to ensure
    // that all resources will be disposed.
    UpdateDatabase(scope.ServiceProvider);
}
Console.ReadKey();

static ServiceProvider CreateServices(ISettingsProvider settingsProvider)
{
    return new ServiceCollection()
        .AddFluentMigratorCore()
        .ConfigureRunner(rb => rb
            .AddSQLite()
            .WithGlobalConnectionString(settingsProvider.GetWorkNotesDataDatabaseConnectionString())
            .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        .BuildServiceProvider(false);
}

static void UpdateDatabase(IServiceProvider serviceProvider)
{
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}