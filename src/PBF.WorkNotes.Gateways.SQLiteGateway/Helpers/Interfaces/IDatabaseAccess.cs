namespace PBF.WorkNotes.Gateways.SQLiteGateway.Helpers.Interfaces;

public interface IDatabaseAccess<M> : IDisposable
{
    IDbConnection OpenConnection();
    void CloseConnection();
    Task<IEnumerable<M>> QueryAsync(string sql);
    Task<M?> QuerySingleOrDefaultAsync(string sql, DynamicParameters parameters);
    Task<Guid> InsertAndGetIdAsync(string sql, DynamicParameters parameters);
    Task<int> ExecuteAsync(string sql, DynamicParameters parameters);
}