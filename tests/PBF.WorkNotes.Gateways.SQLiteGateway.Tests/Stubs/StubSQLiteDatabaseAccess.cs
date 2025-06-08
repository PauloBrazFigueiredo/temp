namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Stubs;

[ExcludeFromCodeCoverage]
public class StubSQLiteDatabaseAccess<M> : SQLiteDatabaseAccess<M>
{
    public StubSQLiteDatabaseAccess(ISettingsProvider settingsProvider) : base(settingsProvider)
    {
    }

    public IDbConnection Connection 
    {
        get => _connection;
        set => _connection = value;
    }
}
