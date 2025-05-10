namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Fixtures;

[ExcludeFromCodeCoverage]
public class TagsSQLiteRepositoryFixture : BaseFixture
{
    private const string DataSourcePrefix = "TagsSQLiteRepository";
    public ITagsRepository SUT { get; set; }

    public TagsSQLiteRepositoryFixture()
    {
        base.Initialize(DataSourcePrefix);
        SUT = ServiceProvider.GetRequiredService<ITagsRepository>();
    }
}
