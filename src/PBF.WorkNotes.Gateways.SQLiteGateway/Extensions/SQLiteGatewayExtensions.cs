namespace PBF.WorkNotes.Gateways.SQLiteGateway.Extensions;

public static class SQLiteGatewayExtensions
{
    public static IServiceCollection AddSQLiteGateway(this IServiceCollection services, ISettingsProvider settingsProvider)
    {
        services.AddTransient<SqliteConnection>(_ => new SqliteConnection(settingsProvider.GetWorkNotesDataDatabaseConnectionString()));
        SqlMapper.AddTypeHandler(new GuidTypeHandler());

        services.AddTransient<IDatabaseAccess<ToDoStateModel>, SQLiteDatabaseAccess<ToDoStateModel>>();
        services.AddTransient<IDatabaseAccess<PriorityModel>, SQLiteDatabaseAccess<PriorityModel>>();
        services.AddTransient<IDatabaseAccess<TagModel>, SQLiteDatabaseAccess<TagModel>>();
        services.AddTransient<IDatabaseAccess<ToDoModel>, SQLiteDatabaseAccess<ToDoModel>>();

        services.AddTransient<IToDoStatesRepository, ToDoStatesSQLiteRepository>();
        services.AddTransient<IPrioritiesRepository, PrioritiesSQLiteRepository>();
        services.AddTransient<ITagsRepository, TagsSQLiteRepository>();
        services.AddTransient<IToDosRepository, ToDosSQLiteRepository>();

        return services;
    }
}