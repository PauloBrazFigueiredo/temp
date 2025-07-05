namespace PBF.WorkNotes.Gateways.SQLiteGateway.Repositories;

public class ToDosSQLiteRepository(
    IDatabaseAccess<ToDoModel> databaseAccess,
    IMapper mapper,
    IGuidProvider guidProvider
    ) : IToDosRepository
{
    public async Task<IEnumerable<ToDo>> GetAll()
    {
        var sql = """
            SELECT
                Id,
                Title,
                Notes,
                StateId,
                PriorityId,
                IsPrivate,
                "Order",
                WorkDate,
                DueDate,
                CreatedDate
            FROM ToDos
        """;
        var models = await databaseAccess.QueryAsync(sql);
        return mapper.Map<IEnumerable<ToDo>>(models);
    }

    public async Task<ToDo> GetById(Guid id)
    {
        var sql = """
            SELECT
                Id,
                Title,
                Notes,
                StateId,
                PriorityId,
                IsPrivate,
                "Order",
                WorkDate,
                DueDate,
                CreatedDate
            FROM ToDos
            WHERE Id = @Id
        """;
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.ToString(), DbType.String, ParameterDirection.Input);
        var model = await databaseAccess.QuerySingleOrDefaultAsync(sql, parameters);

        return mapper.Map<ToDo>(model);
    }

    public async Task<Guid> Create(ToDo entity)
    {
        var sql = """
            INSERT INTO ToDos ("Id", "Title", "Notes", "StateId", "PriorityId", "IsPrivate", "Order", "WorkDate", "DueDate", "CreatedDate")
            VALUES (@Id, @Title, @Notes, @StateId, @PriorityId, @IsPrivate, @Order, @WorkDate, @DueDate, @CreatedDate)
        """;
        var model = mapper.Map<ToDoModel>(entity);
        model.CreatedDate = DateTime.UtcNow;
        var parameters = new DynamicParameters();
        var id = guidProvider.GetGuid();
        parameters.Add("Id", id.ToString(), DbType.String, ParameterDirection.Input);
        parameters.Add("Title", model.Title, DbType.String, ParameterDirection.Input);
        parameters.Add("Notes", model.Notes, DbType.String, ParameterDirection.Input);
        parameters.Add("StateId", model.StateId.ToString(), DbType.String, ParameterDirection.Input);
        parameters.Add("PriorityId", model.PriorityId.ToString(), DbType.String, ParameterDirection.Input);
        parameters.Add("IsPrivate", model.IsPrivate, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("Order", model.Order, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WorkDate", model.WorkDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("DueDate", model.DueDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("CreatedDate", model.CreatedDate, DbType.DateTime, ParameterDirection.Input);

        await databaseAccess.ExecuteAsync(sql, parameters);
        return id;
    }

    public async Task<bool> Update(ToDo entity)
    {
        var sql = """
            UPDATE ToDos
            SET Title = @Title,
                Notes = @Notes,
                StateId = @StateId,
                PriorityId = @PriorityId,
                IsPrivate = @IsPrivate,
                "Order" = @Order,
                WorkDate = @WorkDate,
                DueDate = @DueDate,
                CreatedDate = @CreatedDate
            WHERE Id = @Id
        """;
        var model = mapper.Map<ToDo>(entity);
        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id.ToString(), DbType.String, ParameterDirection.Input);
        parameters.Add("Title", entity.Title, DbType.String, ParameterDirection.Input);
        parameters.Add("Notes", entity.Notes, DbType.String, ParameterDirection.Input);
        parameters.Add("StateId", entity.StateId.ToString(), DbType.String, ParameterDirection.Input);
        parameters.Add("PriorityId", entity.PriorityId.ToString(), DbType.String, ParameterDirection.Input);
        parameters.Add("IsPrivate", model.IsPrivate, DbType.Boolean, ParameterDirection.Input);
        parameters.Add("Order", entity.Order, DbType.Int32, ParameterDirection.Input);
        parameters.Add("WorkDate", entity.WorkDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("DueDate", entity.DueDate, DbType.DateTime, ParameterDirection.Input);
        parameters.Add("CreatedDate", entity.CreatedDate, DbType.DateTime, ParameterDirection.Input);

        var result = await databaseAccess.ExecuteAsync(sql, parameters);
        return result == 1;
    }

    public async Task<bool> Delete(Guid id)
    {
        var sql = """
            DELETE FROM ToDos
            WHERE Id = @Id
        """;
        var parameters = new DynamicParameters();
        parameters.Add("Id", id.ToString(), DbType.String, ParameterDirection.Input);

        var result = await databaseAccess.ExecuteAsync(sql, parameters);
        return result == 1;
    }
}
