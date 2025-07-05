namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Repositories.IntegrationTests;

[ExcludeFromCodeCoverage]
[Trait("Integration Tests", "Gateways")]
[TestCaseOrderer("PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Attributes.FactOrderer", "PBF.WorkNotes.Gateways.SQLiteGateway.Tests")]
public class ToDosSQLiteRepositoryIntegrationTests :  IClassFixture<ToDosSQLiteRepositoryFixture>
{
    private readonly ToDosSQLiteRepositoryFixture _fixture;
    private IEnumerable<Priority> _priorities;
    private IEnumerable<ToDoState> _states;

    public ToDosSQLiteRepositoryIntegrationTests(ToDosSQLiteRepositoryFixture fixture)
    {
        _fixture = fixture;
        _priorities = _fixture.GetAllPriorities();
        _states = _fixture.GetAllToDoStates();
    }

    [Fact, FactOrder(1)]
    public async Task ToDosSQLiteRepository_CreateValidEntity_SchouldReturnId()
    {
        // Arrange
        var entity = new ToDo
        {
            Title = "testTitle",
            Notes = "testNotes",
            StateId = _states.First().Id,
            State = new ToDoState
            {
                Id = this._states.First().Id,
                Name = this._states.First().Name
            },
            PriorityId = _priorities.First().Id,
            Priority = new Priority
            {
                Id = _priorities.First().Id,
                Name = _priorities.First().Name,
                Level = _priorities.First().Level,
                Color = _priorities.First().Color,
                IsDefault = _priorities.First().IsDefault
            },
            Order = 1,
            WorkDate = new DateTime(2023, 10, 1),
            DueDate = new DateTime(2023, 10, 2),
            CreatedDate = new DateTime(2023, 10, 3)
        };

        // Act
        var result = await _fixture.SUT.Create(entity);
        _fixture.TestId = result;

        // Assert
        result.GetType().Should().Be(typeof(Guid));
        result.Should().NotBe(Guid.Empty);
    }

    [Fact, FactOrder(2)]
    public async Task ToDosSQLiteRepository_GetAll_SchouldReturnEntities()
    {
        // Act
        var result = await _fixture.SUT.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<ToDo>>();
        result.Should().HaveCount(1);
        result.Should().ContainSingle(entity => entity.Title == "testTitle");
    }

    [Fact, FactOrder(3)]
    public async Task ToDosSQLiteRepository_GetByIdWithValidId_SchouldReturnEntity()
    {
        // Act
        var result = await _fixture.SUT.GetById(_fixture.TestId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ToDo>();
    }

    [Fact, FactOrder(4)]
    public async Task ToDosSQLiteRepository_GetByIdWithInvalidId_SchouldReturnNull()
    {
        // Act
        var result = await _fixture.SUT.GetById(Guid.Empty);

        // Assert
        result.Should().BeNull();
    }

    [Fact, FactOrder(5)]
    public async Task ToDosSQLiteRepository_UpdateValidEntity_SchouldReturnTrue()
    {
        // Arrange
        var entity = new ToDo
        {
            Id = _fixture.TestId,
            Title = "testTitle1",
            Notes = "testNotes1",
            StateId = _states.First().Id,
            State = new ToDoState
            {
                Id = _states.First().Id,
                Name = _states.First().Name
            },
            PriorityId = _priorities.First().Id,
            Priority = new Priority
            {
                Id = _priorities.First().Id,
                Name = _priorities.First().Name,
                Level = _priorities.First().Level,
                Color = _priorities.First().Color,
                IsDefault = _priorities.First().IsDefault
            },
            Order = 1,
            WorkDate = new DateTime(2023, 10, 1),
            DueDate = new DateTime(2023, 10, 2),
            CreatedDate = new DateTime(2023, 10, 3)
        };

        // Act
        var result = await _fixture.SUT.Update(entity);

        // Assert
        result.Should().BeTrue();
    }

    [Fact, FactOrder(6)]
    public async Task ToDosSQLiteRepository_UpdateInvalidEntity_SchouldReturnFalse()
    {
        // Arrange
        var entity = new ToDo
        {
            Id = Guid.Empty,
            Title = "testTitle1",
            Notes = "testNotes1",
            StateId = _states.First().Id,
            State = new ToDoState
            {
                Id = _states.First().Id,
                Name = _states.First().Name
            },
            PriorityId = _priorities.First().Id,
            Priority = new Priority
            {
                Id = _priorities.First().Id,
                Name = _priorities.First().Name,
                Level = _priorities.First().Level,
                Color = _priorities.First().Color,
                IsDefault = _priorities.First().IsDefault
            },
            Order = 1,
            WorkDate = new DateTime(2023, 10, 1),
            DueDate = new DateTime(2023, 10, 2),
            CreatedDate = new DateTime(2023, 10, 3)
        };

        // Act
        var result = await _fixture.SUT.Update(entity);

        // Assert
        result.Should().BeFalse();
    }

    [Fact, FactOrder(7)]
    public async Task ToDosSQLiteRepository_DeleteValidEntity_SchouldReturnTrue()
    {
        // Act
        var result = await _fixture.SUT.Delete(_fixture.TestId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact, FactOrder(8)]
    public async Task ToDosSQLiteRepository_DeleteInvalidEntity_SchouldReturnFalse()
    {
        // Act
        var result = await _fixture.SUT.Delete(_fixture.TestId);

        // Assert
        result.Should().BeFalse();
    }
}
