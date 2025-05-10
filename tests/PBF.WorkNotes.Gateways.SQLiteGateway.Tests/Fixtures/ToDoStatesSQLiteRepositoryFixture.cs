namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Fixtures;

[ExcludeFromCodeCoverage]
public class ToDoStatesSQLiteRepositoryFixture : BaseFixture
{
    private const string DataSourcePrefix = "ToDoStatesSQLiteRepository";
    public IToDoStatesRepository SUT { get; set; }

    public ToDoStatesSQLiteRepositoryFixture()
    {
        base.Initialize(DataSourcePrefix);
        SUT = ServiceProvider.GetRequiredService<IToDoStatesRepository>();
    }
}
