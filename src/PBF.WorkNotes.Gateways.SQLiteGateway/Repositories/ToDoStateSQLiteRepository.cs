namespace PBF.WorkNotes.Gateways.SQLiteGateway.Repositories;

public  class ToDoStateSQLiteRepository : IToDoStateRepository
{
    private readonly IDatabaseAccess<ToDoStateModel> _databaseAccess;
    private readonly IMapper _mapper;

    public ToDoStateSQLiteRepository(IMapper mapper, IDatabaseAccess<ToDoStateModel> databaseAccess)
    {
        _mapper = mapper;
        _databaseAccess = databaseAccess;
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
        var model = await _databaseAccess.QuerySingleOrDefaultAsync(sql, new { id });
        return _mapper.Map<ToDoState>(model);
    }

    public async Task Create(ToDoState model)
    {
        var sql = """
            INSERT INTO ToDoState (Id, Name, IsDefault)
            VALUES (@Id, @Name, @IsDefault)
        """;
        await _databaseAccess.ExecuteAsync(sql, model);
    }

    public async Task Update(ToDoState model)
    {
        var sql = """
            UPDATE ToDoState 
            SET Id = @Id,
                Name = @Name,
                IsDefault = @IsDefault
            WHERE Id = @Id
        """;
        await _databaseAccess.ExecuteAsync(sql, model);
    }

    public async Task Delete(int id)
    {
        var sql = """
            DELETE FROM ToDoState 
            WHERE Id = @id
        """;
        await _databaseAccess.ExecuteAsync(sql, new { id });
    }
}
