namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Repositories;

[ExcludeFromCodeCoverage]
[Trait("Integration Tests", "Gateways")]
[TestCaseOrderer("PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Attributes.FactOrderer", "PBF.WorkNotes.Gateways.SQLiteGateway.Tests")]
public class PrioritiesSQLiteRepositoryIntegrationTests :  IClassFixture<PrioritiesSQLiteRepositoryFixture>
{
    private readonly PrioritiesSQLiteRepositoryFixture _fixture;

    public PrioritiesSQLiteRepositoryIntegrationTests(PrioritiesSQLiteRepositoryFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact, FactOrder(1)]
    public async Task ToPrioritiesSQLiteRepository_GetAll_SchouldReturnEntities()
    {
        // Act
        var result = await _fixture.SUT.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<List<Priority>>();
        result.Should().HaveCount(4);
        result.Should().ContainSingle(entity => entity.Name == "Critical" && entity.Level == "P0" && entity.Color == "0xff0000" && entity.IsDefault == false);
        result.Should().ContainSingle(entity => entity.Name == "High" && entity.Level == "P1" && entity.Color == "0xff4500" && entity.IsDefault == false);
        result.Should().ContainSingle(entity => entity.Name == "Medium" && entity.Level == "P2" && entity.Color == "0xffd700" && entity.IsDefault == true);
        result.Should().ContainSingle(entity => entity.Name == "Low" && entity.Level == "P3" && entity.Color == "0xa9a9a9" && entity.IsDefault == false);
}

    [Fact, FactOrder(2)]
    public async Task PrioritiesSQLiteRepository_CreateValidEntity_SchouldReturnId()
    {
        // Arrange
        var entity = new Priority { Name = "New", Level = "P4", Color = "0xff0000", IsDefault = false };

        // Act
        var result = await _fixture.SUT.Create(entity);
        _fixture.TestId = result;

        // Assert
        result.GetType().Should().Be(typeof(Guid));
        result.Should().NotBe(Guid.Empty);
    }

    [Fact, FactOrder(3)]
    public async Task PrioritiesSQLiteRepository_GetByIdWithValidId_SchouldReturnEntity()
    {
        // Act
        var result = await _fixture.SUT.GetById(_fixture.TestId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<Priority>();
    }

    [Fact, FactOrder(4)]
    public async Task PrioritiesSQLiteRepository_GetByIdWithInvalidId_SchouldReturnNull()
    {
        // Act
        var result = await _fixture.SUT.GetById(Guid.Empty);

        // Assert
        result.Should().BeNull();
    }

    [Fact, FactOrder(5)]
    public async Task PrioritiesSQLiteRepository_UpdateValidEntity_SchouldReturnTrue()
    {
        // Arrange
        var entity = new Priority { Id = _fixture.TestId, Name = "New2", Level = "P4", Color = "0xff0000", IsDefault = false };

        // Act
        var result = await _fixture.SUT.Update(entity);

        // Assert
        result.Should().BeTrue();
    }

    [Fact, FactOrder(6)]
    public async Task PrioritiesSQLiteRepository_UpdateInvalidEntity_SchouldReturnFalse()
    {
        // Arrange
        var entity = new Priority { Id = Guid.Empty, Name = "New2", Level = "P4", Color = "0xff0000", IsDefault = false };

        // Act
        var result = await _fixture.SUT.Update(entity);

        // Assert
        result.Should().BeFalse();
    }

    [Fact, FactOrder(7)]
    public async Task PrioritiesSQLiteRepository_DeleteValidEntity_SchouldReturnTrue()
    {
        // Act
        var result = await _fixture.SUT.Delete(_fixture.TestId);

        // Assert
        result.Should().BeTrue();
    }

    [Fact, FactOrder(8)]
    public async Task PrioritiesSQLiteRepository_DeleteInvalidEntity_SchouldReturnFalse()
    {
        // Act
        var result = await _fixture.SUT.Delete(_fixture.TestId);

        // Assert
        result.Should().BeFalse();
    }
}
