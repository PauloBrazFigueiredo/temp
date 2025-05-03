namespace PBF.WorkNotes.Gateways.SQLiteGateway.Extensions;

public static class SQLiteGatewayExtensions
{
    public static IServiceCollection AddSQLiteGateway(this IServiceCollection services, ISettingsProvider settingsProvider)
    {
        services.AddTransient<SqliteConnection>(_ => new SqliteConnection(settingsProvider.GetWorkNotesDataDatabaseConnectionString()));

        services.AddTransient<IDatabaseAccess<ToDoStateModel>, SQLiteDatabaseAccess<ToDoStateModel>>();

        services.AddTransient<IToDoStateRepository, ToDoStateSQLiteRepository>();

        return services;
    }
}
