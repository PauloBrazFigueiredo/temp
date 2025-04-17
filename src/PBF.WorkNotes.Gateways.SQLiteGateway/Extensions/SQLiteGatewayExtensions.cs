namespace PBF.WorkNotes.Gateways.SQLiteGateway.Extensions;

public static class SQLiteGatewayExtensions
{
    public static IServiceCollection AddSQLiteGateway(this IServiceCollection services, ISettingsProvider settingsProvider)
    {
        //TODO: To change.
        //services.AddTransient<DataContext>(_ => new DataContext(settingsProvider.Settings));

        services.AddTransient<SqliteConnection>(_ => new SqliteConnection(settingsProvider.GetWorkNotesDataDatabaseConnectionString()));

        services.AddTransient<IToDoStateRepository, ToDoStateSQLiteRepository>();

        return services;
    }
}
