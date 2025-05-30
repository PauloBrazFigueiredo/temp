namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Fixtures;

[ExcludeFromCodeCoverage]
public class ToDosSQLiteRepositoryFixture : BaseFixture
{
    private const string DataSourcePrefix = "ToDosSQLiteRepository";
    private IPrioritiesRepository _prioritiesRepository;
    private IToDoStatesRepository _toDoStatesRepository;
    public IToDosRepository SUT { get; set; }

    public ToDosSQLiteRepositoryFixture()
    {
        base.Initialize(DataSourcePrefix);
        SUT = ServiceProvider.GetRequiredService<IToDosRepository>();
        _prioritiesRepository = ServiceProvider.GetRequiredService<IPrioritiesRepository>();
        _toDoStatesRepository = ServiceProvider.GetRequiredService<IToDoStatesRepository>();
    }

    public IEnumerable<Priority> GetAllPriorities()
    {
        return _prioritiesRepository.GetAll().GetAwaiter().GetResult();
    }

    public IEnumerable<ToDoState> GetAllToDoStates()
    {
        return _toDoStatesRepository.GetAll().GetAwaiter().GetResult();
    }
}
