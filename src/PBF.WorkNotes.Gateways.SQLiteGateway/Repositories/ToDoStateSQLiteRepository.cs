namespace PBF.WorkNotes.Gateways.SQLiteGateway.Repositories;

public  class ToDoStateSQLiteRepository : IToDoStateRepository
{
    private readonly IDatabaseAccess<ToDoStateModel, Guid> _databaseAccess;
    private readonly IMapper _mapper;
    private readonly IGuidProvider _guidProvider;

    public ToDoStateSQLiteRepository(IMapper mapper, IGuidProvider guidProvider, IDatabaseAccess<ToDoStateModel, Guid> databaseAccess)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _guidProvider = guidProvider ?? throw new ArgumentNullException(nameof(guidProvider));
        _databaseAccess = databaseAccess ?? throw new ArgumentNullException(nameof(databaseAccess));
    }

    public async Task<IEnumerable<ToDoState>> GetAll()
    {
        var sql = """
            SELECT
                Id,
                Name,
                IsDefault
            FROM ToDoState
        """;
        var models = await _databaseAccess.QueryAsync(sql);
        return _mapper.Map<IEnumerable<ToDoState>>(models);
    }

    public async Task<ToDoState> GetById(Guid id)
    {
        var sql = """
            SELECT
                Id,
                Name,
                IsDefault
            FROM ToDoState
            WHERE Id = @id
        """;
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        var model = await _databaseAccess.QuerySingleOrDefaultAsync(sql, parameters);

        return _mapper.Map<ToDoState>(model);
    }

    public async Task<Guid> Create(ToDoState entity)
    {
    var sql = """
            INSERT INTO ToDoState (Id, Name, IsDefault)
            VALUES (@Id, @Name, @IsDefault)
        """;
        var model = _mapper.Map<ToDoStateModel>(entity);
        var parameters = new DynamicParameters();
        var id = _guidProvider.GetGuid();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Output);
        parameters.Add("Name", model.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("IsDefault", model.IsDefault, DbType.Boolean, ParameterDirection.Input);

        return await _databaseAccess.InsertAndGetIdAsync(sql, parameters);
    }

    public async Task<int> Update(ToDoState entity)
    {
        var sql = """
            UPDATE ToDoState 
            SET Name = @Name,
                IsDefault = @IsDefault
            WHERE Id = @Id
        """;
        var model = _mapper.Map<ToDoStateModel>(entity);
        var parameters = new DynamicParameters();
        parameters.Add("Id", model.Id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("Name", model.Name, DbType.String, ParameterDirection.Input);
        parameters.Add("IsDefault", model.IsDefault, DbType.Boolean, ParameterDirection.Input);

        return await _databaseAccess.ExecuteAsync(sql, parameters);
    }

    public async Task<int> Delete(Guid id)
    {
        var sql = """
            DELETE FROM ToDoState 
            WHERE Id = @Id
        """;
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);

        return await _databaseAccess.ExecuteAsync(sql, parameters);
    }
}
