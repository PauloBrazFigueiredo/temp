namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Repositories.UnitTests;

[ExcludeFromCodeCoverage]
public class BaseSQLiteRepositoryUnitTests
{
    protected IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        return config.CreateMapper();
    }

    protected IGuidProvider CreateGuidProvider()
    {
        var mock = new Mock<IGuidProvider>();
        mock.Setup(mock => mock.GetGuid()).Returns(Guid.NewGuid());
        return mock.Object;
    }   
}
