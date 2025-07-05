namespace PBF.WorkNotes.Gateways.SQLiteGateway.Repositories;

public  class ToDoStatesSQLiteRepository(
    IDatabaseAccess<ToDoStateModel> databaseAccess,
    IMapper mapper,
    IGuidProvider guidProvider
    ) : IToDoStatesRepository
{
    public async Task<IEnumerable<ToDoState>> GetAll()
    {
        var sql = """
            SELECT
                Id,
                Name,
                IsDefault
            FROM ToDoStates
        """;
        var models = await databaseAccess.QueryAsync(sql);
        return mapper.Map<IEnumerable<ToDoState>>(models);
    }

    public async Task<ToDoState> GetById(Guid id)
    {
        var sql = """
            SELECT
                Id,
                Name,
                IsDefault
            FROM ToDoStates
            WHERE Id = @Id
        """;
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.ToString(), DbType.String, ParameterDirection.Input);

        var model = await databaseAccess.QuerySingleOrDefaultAsync(sql, parameters);
        return mapper.Map<ToDoState>(model);
    }

    public async Task<Guid> Create(ToDoState entity)
    {
    var sql = """
            INSERT INTO ToDoStates (Id, Name, IsDefault)
            VALUES (@Id, @Name, @IsDefault)
        """;
        var model = mapper.Map<ToDoStateModel>(entity);
        var parameters = new DynamicParameters();
        var id = guidProvider.GetGuid();
        parameters.Add("Id", id.ToString(), DbType.String, ParameterDirection.Input);
        parameters.Add("Name", model.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("IsDefault", model.IsDefault, DbType.Boolean, ParameterDirection.Input);

        await databaseAccess.ExecuteAsync(sql, parameters);
        return id;
    }

    public async Task<bool> Update(ToDoState entity)
    {
        var sql = """
            UPDATE ToDoStates
            SET Name = @Name,
                IsDefault = @IsDefault
            WHERE Id = @Id
        """;
        var model = mapper.Map<ToDoStateModel>(entity);
        var parameters = new DynamicParameters();
        parameters.Add("Id", model.Id, DbType.String, ParameterDirection.Input);
        parameters.Add("Name", model.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("IsDefault", model.IsDefault, DbType.Boolean, ParameterDirection.Input);

        var result = await databaseAccess.ExecuteAsync(sql, parameters);
        return result == 1;
    }

    public async Task<bool> Delete(Guid id)
    {
        var sql = """
            DELETE FROM ToDoStates
            WHERE Id = @Id
        """;
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.ToString(), DbType.String, ParameterDirection.Input);

        var result = await databaseAccess.ExecuteAsync(sql, parameters);
        return result == 1;
    }
}
