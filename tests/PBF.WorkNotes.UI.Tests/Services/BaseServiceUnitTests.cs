namespace PBF.WorkNotes.UI.Tests.Services;

[ExcludeFromCodeCoverage]
public abstract class BaseServiceUnitTests
{
    protected IMapper Mapper { get; set; }

    protected BaseServiceUnitTests()
    {
        Mapper = CreateMapper();
    }

    protected IMapper CreateMapper()
    {
        var config = new MapperConfiguration(config =>
        {
            config.AddProfile<UIMappingProfile>();
        },
        new NullLoggerFactory());
        return config.CreateMapper();
    }
}
