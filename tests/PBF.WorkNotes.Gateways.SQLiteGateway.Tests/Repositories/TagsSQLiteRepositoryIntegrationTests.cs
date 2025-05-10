namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Repositories;

[ExcludeFromCodeCoverage]
[Trait("Integration Tests", "Gateways")]
[TestCaseOrderer("PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Attributes.FactOrderer", "PBF.WorkNotes.Gateways.SQLiteGateway.Tests")]
public class TagsSQLiteRepositoryIntegrationTests :  IClassFixture<TagsSQLiteRepositoryFixture>
{
    private readonly TagsSQLiteRepositoryFixture _fixture;

    public TagsSQLiteRepositoryIntegrationTests(TagsSQLiteRepositoryFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact, FactOrder(1)]
    public async Task TagsSQLiteRepository_CreateValidEntity_SchouldReturnIds()
    {
        // Arrange
        var entity1 = new Tag { IsPermanent = true, Name = "testName1" };
        var entity2 = new Tag { IsPermanent = false, Name = "testName2" };

        // Act
        var result1 = await _fixture.SUT.Create(entity1);
        _fixture.TestId = result1;
        var result2 = await _fixture.SUT.Create(entity2);

        // Assert
        result1.GetType().Should().Be(typeof(Guid));
        result1.Should().NotBe(Guid.Empty);
        result2.GetType().Should().Be(typeof(Guid));
        result2.Should().NotBe(Guid.Empty);
    }

    [Fact, FactOrder(2)]
    public async Task TagsSQLiteRepository_GetAll_SchouldReturnEntities()
    {
        // Act
        var result = await _fixture.SUT.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<Tag>>();
        result.Should().HaveCount(2);
        result.Should().ContainSingle(entity => entity.Name == "testName1" && entity.IsPermanent);
        result.Should().ContainSingle(entity => entity.Name == "testName2" && !entity.IsPermanent);
    }

    [Fact, FactOrder(3)]
    public async Task TagsSQLiteRepository_GetByIdWithValidId_SchouldReturnEntity()
    {
        // Act
        var result = await _fixture.SUT.GetById(_fixture.TestId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Tag>();
    }

    [Fact, FactOrder(4)]
    public async Task TagsSQLiteRepository_GetByIdWithInvalidId_SchouldReturnNull()
    {
        // Act
        var result = await _fixture.SUT.GetById(Guid.Empty);

        // Assert
        result.Should().BeNull();
    }

    [Fact, FactOrder(5)]
    public async Task TagsSQLiteRepository_UpdateValidEntity_SchouldReturnTrue()
    {
        // Arrange
        var entity = new Tag { Id = _fixture.TestId, IsPermanent = false, Name = "testName1" };

        // Act
        var result = await _fixture.SUT.Update(entity);

        // Assert
        result.Should().BeTrue();
    }

    [Fact, FactOrder(6)]
    public async Task TagsSQLiteRepository_UpdateValidEntity_SchouldReturnFalse()
    {
        // Arrange
        var entity = new Tag { Id = Guid.Empty, IsPermanent = false, Name = "testName1" };

        // Act
        var result = await _fixture.SUT.Update(entity);

        // Assert
        result.Should().BeFalse();
    }

    [Fact, FactOrder(7)]
    public async Task TagsSQLiteRepository_DeleteValidEntity_SchouldReturnTrue()
    {
        // Act
        var result = await _fixture.SUT.Delete(_fixture.TestId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact, FactOrder(8)]
    public async Task TagsSQLiteRepository_DeleteInvalidEntity_SchouldReturnFalse()
    {
        // Act
        var result = await _fixture.SUT.Delete(_fixture.TestId);

        // Assert
        result.Should().BeFalse();
    }
}
