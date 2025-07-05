namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Repositories.UnitTests;

[ExcludeFromCodeCoverage]
public abstract class BaseSQLiteRepositoryUnitTests
{
    protected IMapper CreateMapper()
    {
        var config = new MapperConfiguration(config =>
        {
            config.AddProfile<SQLiteGatewayMappingProfile>();
        },
        new NullLoggerFactory());
        return config.CreateMapper();
    }

    protected IGuidProvider CreateGuidProvider()
    {
        var mock = new Mock<IGuidProvider>();
        mock.Setup(mock => mock.GetGuid()).Returns(Guid.NewGuid());
        return mock.Object;
    }   
}
