namespace PBF.WorkNotes.Gateways.SQLiteGateway.Helpers;

public interface IDatabaseAccess<T>
{
    IDbConnection OpenConnection();
    Task<IEnumerable<T>> QueryAsync(string sql);
    Task<T?> QuerySingleOrDefaultAsync(string sql, object param);
    Task ExecuteAsync(string sql, object param);
}

public  class SQLiteDatabaseAccess<T>: IDatabaseAccess<T>, IDisposable
{
    private readonly AppSettings _settings;
    private IDbConnection _connection;

    public SQLiteDatabaseAccess(AppSettings settings)
    {
        _settings = settings;
    }

    public IDbConnection OpenConnection()
    {
        if(_connection is null)
        {
            _connection = new SqliteConnection(SettingsProvider.GetConnectionString(_settings, "WorkNotesData"));
        }

        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
        }

        _connection = new SqliteConnection(SettingsProvider.GetConnectionString(_settings, "WorkNotesData"));
        return _connection;
    }

    public async Task<IEnumerable<T>> QueryAsync(string sql)
    {
        _connection = OpenConnection();
        return await _connection.QueryAsync<T>(sql);
    }

    public async Task<T?> QuerySingleOrDefaultAsync(string sql, object param)
    {
        _connection = OpenConnection();
        return await _connection.QuerySingleOrDefaultAsync<T>(sql, param);
    }

    public async Task ExecuteAsync(string sql, object param)
    {
        _connection = OpenConnection();
        await _connection.ExecuteAsync(sql, param);
    }
    
    public void Dispose()
    {
        if (_connection.State != ConnectionState.Closed)
        {
            _connection.Close();
        }
        _connection.Dispose();
    }
}
