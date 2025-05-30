namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Fixtures;

[ExcludeFromCodeCoverage]
public class PrioritiesSQLiteRepositoryFixture : BaseFixture
{
    private const string DataSourcePrefix = "PrioritiesSQLiteRepository";
    public IPrioritiesRepository SUT { get; set; }

    public PrioritiesSQLiteRepositoryFixture()
    {
        base.Initialize(DataSourcePrefix);
        SUT = ServiceProvider.GetRequiredService<IPrioritiesRepository>();
    }
}
