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
                Description,
                StateId,
                PriorityId,
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
                Description,
                StateId,
                PriorityId,
                "Order",
                WorkDate,
                DueDate,
                CreatedDate
            FROM ToDos
            WHERE Id = @Id
        """;
        var parameters = new DynamicParameters();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        var model = await databaseAccess.QuerySingleOrDefaultAsync(sql, parameters);

        return mapper.Map<ToDo>(model);
    }

    public async Task<Guid> Create(ToDo entity)
    {
        var sql = """
            INSERT INTO ToDos ("Id", "Title", "Description", "StateId", "PriorityId", "Order", "WorkDate", "DueDate", "CreatedDate")
            VALUES (@Id, @Title, @Description, @StateId, @PriorityId, @Order, @WorkDate, @DueDate, @CreatedDate)
        """;
        var model = mapper.Map<ToDoModel>(entity);
        var parameters = new DynamicParameters();
        var id = guidProvider.GetGuid();
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("Title", model.Title, DbType.String, ParameterDirection.Input);
        parameters.Add("Description", model.Description, DbType.String, ParameterDirection.Input);
        parameters.Add("StateId", model.StateId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("PriorityId", model.PriorityId, DbType.Guid, ParameterDirection.Input);
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
                Description = @Description,
                StateId = @StateId,
                PriorityId = @PriorityId,
                "Order" = @Order,
                WorkDate = @WorkDate,
                DueDate = @DueDate,
                CreatedDate = @CreatedDate
            WHERE Id = @Id
        """;
        var model = mapper.Map<ToDo>(entity);
        var parameters = new DynamicParameters();
        parameters.Add("Id", entity.Id, DbType.Guid, ParameterDirection.Input);
        parameters.Add("Title", entity.Title, DbType.String, ParameterDirection.Input);
        parameters.Add("Description", entity.Description, DbType.String, ParameterDirection.Input);
        parameters.Add("StateId", entity.StateId, DbType.Guid, ParameterDirection.Input);
        parameters.Add("PriorityId", entity.PriorityId, DbType.Guid, ParameterDirection.Input);
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
        parameters.Add("Id", id, DbType.Guid, ParameterDirection.Input);

        var result = await databaseAccess.ExecuteAsync(sql, parameters);
        return result == 1;
    }
}
