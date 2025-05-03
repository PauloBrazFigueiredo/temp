namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Fixtures;

[ExcludeFromCodeCoverage]
public class ToDoStateSQLiteRepositoryFixture : BaseFixture
{
    private const string DataSourcePrefix = "ToDoStateSQLiteRepository";
    public IToDoStateRepository SUT { get; set; }

    public ToDoStateSQLiteRepositoryFixture()
    {
        base.Initialize(DataSourcePrefix);
        SUT = ServiceProvider.GetRequiredService<IToDoStateRepository>();
    }
}
