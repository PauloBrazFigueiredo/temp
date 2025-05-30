namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Repositories;

[ExcludeFromCodeCoverage]
[Trait("Unit Tests", "Gateways")]
public class PrioritiesSQLiteRepositoryUnitTests : BaseSQLiteRepositoryUnitTests
{
    private readonly IMapper _mapper;
    private readonly IGuidProvider _guidProvider;

    public PrioritiesSQLiteRepositoryUnitTests()
    {
        _mapper = CreateMapper();
        _guidProvider = CreateGuidProvider();
    }

    [Fact]
    public void PrioritiesSQLiteRepository_Constructor_SchouldCreateInstance()
    {
        // Arrange
        var mockDatabaseAccess = new Mock<IDatabaseAccess<PriorityModel>>();

        // Act
        var sut = new PrioritiesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Assert
        sut.Should().NotBeNull();
        sut.Should().BeOfType<PrioritiesSQLiteRepository>();
        sut.Should().BeAssignableTo<IPrioritiesRepository>();
    }

    [Fact]
    public async Task PrioritiesSQLiteRepository_GetAll_ShouldReturnEntities()
    {
        // Arrange
        var mockDatabaseAccess = new Mock<IDatabaseAccess<PriorityModel>>();
        mockDatabaseAccess.Setup(mock => mock.QueryAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<PriorityModel>());
        var sut = new PrioritiesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.GetAll();

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QueryAsync("""
            SELECT
                Id,
                Name,
                Level,
                Color,
                IsDefault
            FROM Priorities
        """), Times.Once);
    }

    [Fact]
    public async Task PrioritiesSQLiteRepository_GetByIdValidEntity_ShouldReturnEntity()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<PriorityModel>>();
        mockDatabaseAccess.Setup(mock => mock.QuerySingleOrDefaultAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(new PriorityModel());
        var sut = new PrioritiesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.GetById(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QuerySingleOrDefaultAsync("""
            SELECT
                Id,
                Name,
                Level,
                Color,
                IsDefault
            FROM Priorities
            WHERE Id = @Id
        """, 
            It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") ==id)),
            Times.Once);
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task PrioritiesSQLiteRepository_GetByIdInvalidEntity_ShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<PriorityModel>>();
        mockDatabaseAccess.Setup(mock => mock.QuerySingleOrDefaultAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync((PriorityModel)null);
        var sut = new PrioritiesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.GetById(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QuerySingleOrDefaultAsync("""
            SELECT
                Id,
                Name,
                Level,
                Color,
                IsDefault
            FROM Priorities
            WHERE Id = @Id
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == id)),
            Times.Once);
        result.Should().BeNull();
    }

    [Fact]
    public async Task PrioritiesSQLiteRepository_CreateValidEntity_ShouldReturnGuid()
    {
        // Arrange
        var entity = new Priority { Name = "Critical", Level = "P0", Color = "0xff0000", IsDefault = true };
        var mockDatabaseAccess = new Mock<IDatabaseAccess<PriorityModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(1);
        var sut = new PrioritiesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Create(entity);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            INSERT INTO Priorities (Id, Name, Level, Color, IsDefault)
            VALUES (@Id, @Name, @Level, @Color, @IsDefault)
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<string>("Name") == entity.Name
                && p.Get<string>("Level") == entity.Level
                && p.Get<string>("Color") == entity.Color
                && p.Get<bool>("IsDefault") == entity.IsDefault)),
            Times.Once);
    }

    [Fact]
    public async Task PrioritiesSQLiteRepository_UpdateValidEntity_ShouldReturnTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new Priority { Name = "Critical", Level = "P0", Color = "0xff0000", IsDefault = true };

        var mockDatabaseAccess = new Mock<IDatabaseAccess<PriorityModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(1);
        var sut = new PrioritiesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Update(entity);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            UPDATE Priorities
            SET Name = @Name,
                Level = @Level,
                Color = @Color,
                IsDefault = @IsDefault
            WHERE Id = @Id
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == entity.Id
                && p.Get<string>("Name") == entity.Name
                && p.Get<string>("Level") == entity.Level
                && p.Get<string>("Color") == entity.Color
                && p.Get<bool>("IsDefault") == entity.IsDefault)),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task PrioritiesSQLiteRepository_UpdateInvalidEntity_ShouldReturnFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new Priority { Name = "Critical", Level = "P0", Color = "0xff0000", IsDefault = true };

        var mockDatabaseAccess = new Mock<IDatabaseAccess<PriorityModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(0);
        var sut = new PrioritiesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Update(entity);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            UPDATE Priorities
            SET Name = @Name,
                Level = @Level,
                Color = @Color,
                IsDefault = @IsDefault
            WHERE Id = @Id
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == entity.Id
                && p.Get<string>("Name") == entity.Name
                && p.Get<string>("Level") == entity.Level
                && p.Get<string>("Color") == entity.Color
                && p.Get<bool>("IsDefault") == entity.IsDefault)),
            Times.Once);
        result.Should().BeFalse();
    }

    [Fact]
    public async Task PrioritiesSQLiteRepository_DeleteValidEntity_ShouldReturnTrue()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<PriorityModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(1);
        var sut = new PrioritiesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Delete(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            DELETE FROM Priorities
            WHERE Id = @Id
        """,
        It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == id)),
        Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task PrioritiesSQLiteRepository_DeleteInvalidEntity_ShouldReturnFalse()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<PriorityModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(0);
        var sut = new PrioritiesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Delete(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            DELETE FROM Priorities
            WHERE Id = @Id
        """,
        It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == id)),
        Times.Once);
        result.Should().BeFalse();
    }
}
