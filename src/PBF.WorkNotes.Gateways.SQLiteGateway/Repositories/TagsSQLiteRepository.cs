namespace PBF.WorkNotes.Gateways.SQLiteGateway.Repositories;

public  class TagsSQLiteRepository(
    IDatabaseAccess<TagModel> databaseAccess,
    IMapper mapper,
    IGuidProvider guidProvider
    ) : ITagsRepository
{
    public async Task<IEnumerable<Tag>> GetAll()
    {
        var sql = """
            SELECT
                Id,
                Name,
                IsPermanent
            FROM Tags
        """;
        var models = await databaseAccess.QueryAsync(sql);
        return mapper.Map<IEnumerable<Tag>>(models);
    }

    public async Task<Tag> GetById(Guid id)
    {
        var sql = """
            SELECT
                Id,
                Name,
                IsPermanent
            FROM Tags
            WHERE Id = @Id
        """;
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        var model = await databaseAccess.QuerySingleOrDefaultAsync(sql, parameters);

        return mapper.Map<Tag>(model);
    }

    public async Task<Guid> Create(Tag entity)
    {
    var sql = """
            INSERT INTO Tags (Id, Name, IsPermanent)
            VALUES (@Id, @Name, @IsPermanent)
        """;
        var model = mapper.Map<TagModel>(entity);
        var parameters = new DynamicParameters();
        var id = guidProvider.GetGuid();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("Name", model.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("IsPermanent", model.IsPermanent, DbType.Boolean, ParameterDirection.Input);

        await databaseAccess.ExecuteAsync(sql, parameters);
        return id;
    }

    public async Task<bool> Update(Tag entity)
    {
        var sql = """
            UPDATE Tags
            SET Name = @Name,
                IsPermanent = @IsPermanent
            WHERE Id = @Id
        """;
        var model = mapper.Map<TagModel>(entity);
        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("Name", model.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("IsPermanent", model.IsPermanent, DbType.Boolean, ParameterDirection.Input);

        var result = await databaseAccess.ExecuteAsync(sql, parameters);
        return result == 1;
    }

    public async Task<bool> Delete(Guid id)
    {
        var sql = """
            DELETE FROM Tags
            WHERE Id = @Id
        """;
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);

        var result = await databaseAccess.ExecuteAsync(sql, parameters);
        return result == 1;
    }
}
