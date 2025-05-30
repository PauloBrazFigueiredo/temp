namespace PBF.WorkNotes.Gateways.SQLiteGateway.Helpers;

public interface IDatabaseAccess<M> : IDisposable
{
    IDbConnection OpenConnection();
    void CloseConnection();
    Task<IEnumerable<M>> QueryAsync(string sql);
    Task<M?> QuerySingleOrDefaultAsync(string sql, DynamicParameters parameters);
    Task<Guid> InsertAndGetIdAsync(string sql, DynamicParameters parameters);
    Task<int> ExecuteAsync(string sql, DynamicParameters parameters);
}

public  class SQLiteDatabaseAccess<M>: IDatabaseAccess<M>
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

        _connection.Execute("PRAGMA foreign_keys = OFF;");
        return _connection;
    }

    public void CloseConnection()
    {
        if (_connection is not null && _connection.State != ConnectionState.Closed)
        {
            _connection.Close();
        }
        _connection.Dispose();
    }

    public async Task<IEnumerable<M>> QueryAsync(string sql)
    {
        _connection = OpenConnection();
        return await _connection.QueryAsync<M>(sql);
    }

    public async Task<M?> QuerySingleOrDefaultAsync(string sql, DynamicParameters parameters)
    {
        _connection = OpenConnection();
        return await _connection.QuerySingleOrDefaultAsync<M>(sql, parameters);
    }

    public async Task<Guid> InsertAndGetIdAsync(string sql, DynamicParameters parameters)
    {
        var query = $"{sql};";
        _connection = OpenConnection();
        await _connection.QuerySingleAsync(query, parameters);
        return Guid.Parse(parameters.Get<string>("@Id"));
    }

    public async Task<int> ExecuteAsync(string sql, DynamicParameters parameters)
    {
        _connection = OpenConnection();
        return await _connection.ExecuteAsync(sql, parameters);
    }

    public void Dispose()
    {
        CloseConnection();
        _connection?.Dispose();
    }
}
