namespace PBF.WorkNotes.Gateways.SQLiteGateway.Helpers;

public interface IDatabaseAccess<T, K>
{
    IDbConnection OpenConnection();
    Task<IEnumerable<T>> QueryAsync(string sql);
    Task<T?> QuerySingleOrDefaultAsync(string sql, DynamicParameters parameters);
    Task<K> InsertAndGetIdAsync(string sql, DynamicParameters parameters);
    Task<int> ExecuteAsync(string sql, DynamicParameters parameters);
}

public  class SQLiteDatabaseAccess<T, K>: IDatabaseAccess<T, K>, IDisposable
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

    public async Task<T?> QuerySingleOrDefaultAsync(string sql, DynamicParameters parameters)
    {
        _connection = OpenConnection();
        return await _connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
    }

    public async Task<K> InsertAndGetIdAsync(string sql, DynamicParameters parameters)
    {
        var query = $"{sql}; SELECT last_insert_rowid();";
        _connection = OpenConnection();
        var id = await _connection.QuerySingleAsync<K>(query, parameters);
        return id;
    }

    public async Task<int> ExecuteAsync(string sql, DynamicParameters parameters)
    {
        _connection = OpenConnection();
        return await _connection.ExecuteAsync(sql, parameters);
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
