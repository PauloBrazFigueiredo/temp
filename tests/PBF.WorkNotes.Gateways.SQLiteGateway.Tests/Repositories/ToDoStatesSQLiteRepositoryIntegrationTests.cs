namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Repositories;

[ExcludeFromCodeCoverage]
[Trait("Integration Tests", "Gateways")]
[TestCaseOrderer("PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Attributes.FactOrderer", "PBF.WorkNotes.Gateways.SQLiteGateway.Tests")]
public class ToDoStatesSQLiteRepositoryIntegrationTests :  IClassFixture<ToDoStatesSQLiteRepositoryFixture>
{
    private readonly ToDoStatesSQLiteRepositoryFixture _fixture;
    private readonly IMapper _mapper;
    private readonly IGuidProvider _guidProvider;

    public ToDoStatesSQLiteRepositoryIntegrationTests(ToDoStatesSQLiteRepositoryFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact, FactOrder(1)]
    public async Task ToDoStatesSQLiteRepository_GetAll_SchouldReturnEntities()
    {
        // Act
        var result = await _fixture.SUT.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<ToDoState>>();
        result.Should().HaveCount(2);
        result.Should().ContainSingle(entity => entity.Name == "Active");
        result.Should().ContainSingle(entity => entity.Name == "Done");
    }

    [Fact, FactOrder(2)]
    public async Task ToDoStatesSQLiteRepository_CreateValidEntity_SchouldReturnId()
    {
        // Arrange
        var entity = new ToDoState { IsDefault = true, Name = "testName" };

        // Act
        var result = await _fixture.SUT.Create(entity);
        _fixture.TestId = result;

        // Assert
        result.GetType().Should().Be(typeof(Guid));
        result.Should().NotBe(Guid.Empty);
    }

    [Fact, FactOrder(3)]
    public async Task ToDoStatesSQLiteRepository_GetByIdWithValidId_SchouldReturnEntity()
    {
        // Act
        var result = await _fixture.SUT.GetById(_fixture.TestId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ToDoState>();
    }

    [Fact, FactOrder(4)]
    public async Task ToDoStatesSQLiteRepository_GetByIdWithInvalidId_SchouldReturnNull()
    {
        // Act
        var result = await _fixture.SUT.GetById(Guid.Empty);

        // Assert
        result.Should().BeNull();
    }

    [Fact, FactOrder(5)]
    public async Task ToDoStatesSQLiteRepository_UpdateValidEntity_SchouldReturnTrue()
    {
        // Arrange
        var entity = new ToDoState { Id =_fixture.TestId, IsDefault = true, Name = "testName1" };

        // Act
        var result = await _fixture.SUT.Update(entity);

        // Assert
        result.Should().BeTrue();
    }

    [Fact, FactOrder(6)]
    public async Task ToDoStatesSQLiteRepository_UpdateValidEntity_SchouldReturnFalse()
    {
        // Arrange
        var entity = new ToDoState { Id = Guid.Empty, IsDefault = true, Name = "testName1" };

        // Act
        var result = await _fixture.SUT.Update(entity);

        // Assert
        result.Should().BeFalse();
    }

    [Fact, FactOrder(7)]
    public async Task ToDoStatesSQLiteRepository_DeleteValidEntity_SchouldReturnTrue()
    {
        // Act
        var result = await _fixture.SUT.Delete(_fixture.TestId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact, FactOrder(8)]
    public async Task ToDoStatesSQLiteRepository_DeleteInvalidEntity_SchouldReturnFalse()
    {
        // Act
        var result = await _fixture.SUT.Delete(_fixture.TestId);

        // Assert
        result.Should().BeFalse();
    }
}
