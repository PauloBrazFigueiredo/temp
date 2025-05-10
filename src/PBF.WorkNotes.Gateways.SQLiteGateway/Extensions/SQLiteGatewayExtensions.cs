namespace PBF.WorkNotes.Gateways.SQLiteGateway.Extensions;

public static class SQLiteGatewayExtensions
{
    public static IServiceCollection AddSQLiteGateway(this IServiceCollection services, ISettingsProvider settingsProvider)
    {
        services.AddTransient<SqliteConnection>(_ => new SqliteConnection(settingsProvider.GetWorkNotesDataDatabaseConnectionString()));

        services.AddTransient<IDatabaseAccess<ToDoStateModel>, SQLiteDatabaseAccess<ToDoStateModel>>();
        services.AddTransient<IDatabaseAccess<TagModel>, SQLiteDatabaseAccess<TagModel>>();

        services.AddTransient<IToDoStatesRepository, ToDoStatesSQLiteRepository>();
        services.AddTransient<ITagsRepository, TagsSQLiteRepository>();

        return services;
    }
}
