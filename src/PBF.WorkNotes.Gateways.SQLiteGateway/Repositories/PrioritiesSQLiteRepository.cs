namespace PBF.WorkNotes.Gateways.SQLiteGateway.Repositories;

public  class PrioritiesSQLiteRepository(
    IDatabaseAccess<PriorityModel> databaseAccess,
    IMapper mapper,
    IGuidProvider guidProvider
    ) : IPrioritiesRepository
{
    public async Task<IEnumerable<Priority>> GetAll()
    {
        var sql = """
            SELECT
                Id,
                Name,
                Level,
                Color,
                IsDefault
            FROM Priorities
        """;
        var models = await databaseAccess.QueryAsync(sql);
        return mapper.Map<IEnumerable<Priority>>(models);
    }

    public async Task<Priority> GetById(Guid id)
    {
        var sql = """
            SELECT
                Id,
                Name,
                Level,
                Color,
                IsDefault
            FROM Priorities
            WHERE Id = @Id
        """;
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        var model = await databaseAccess.QuerySingleOrDefaultAsync(sql, parameters);

        return mapper.Map<Priority>(model);
    }

    public async Task<Guid> Create(Priority entity)
    {
    var sql = """
            INSERT INTO Priorities (Id, Name, Level, Color, IsDefault)
            VALUES (@Id, @Name, @Level, @Color, @IsDefault)
        """;
        var model = mapper.Map<PriorityModel>(entity);
        var parameters = new DynamicParameters();
        var id = guidProvider.GetGuid();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("Name", model.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("Level", model.Level, DbType.String, ParameterDirection.Input);
        parameters.Add("Color", model.Color, DbType.String, ParameterDirection.Input);
        parameters.Add("IsDefault", model.IsDefault, DbType.Boolean, ParameterDirection.Input);

        await databaseAccess.ExecuteAsync(sql, parameters);
        return id;
    }

    public async Task<bool> Update(Priority entity)
    {
        var sql = """
            UPDATE Priorities
            SET Name = @Name,
                Level = @Level,
                Color = @Color,
                IsDefault = @IsDefault
            WHERE Id = @Id
        """;
        var model = mapper.Map<PriorityModel>(entity);
        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("Name", model.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("Level", model.Level, DbType.String, ParameterDirection.Input);
        parameters.Add("Color", model.Color, DbType.String, ParameterDirection.Input);
        parameters.Add("IsDefault", model.IsDefault, DbType.Boolean, ParameterDirection.Input);

        var result = await databaseAccess.ExecuteAsync(sql, parameters);
        return result == 1;
    }

    public async Task<bool> Delete(Guid id)
    {
        var sql = """
            DELETE FROM Priorities
            WHERE Id = @Id
        """;
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);

        var result = await databaseAccess.ExecuteAsync(sql, parameters);
        return result == 1;
    }
}
