namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Repositories.UnitTests;

[ExcludeFromCodeCoverage]
[Trait("Unit Tests", "Gateways")]
public class TagsSQLiteRepositoryUnitTests : BaseSQLiteRepositoryUnitTests
{
    private readonly IMapper _mapper;
    private readonly IGuidProvider _guidProvider;

    public TagsSQLiteRepositoryUnitTests()
    {
        _mapper = CreateMapper();
        _guidProvider = CreateGuidProvider();
    }

    [Fact]
    public void TagsSQLiteRepository_Constructor_SchouldCreateInstance()
    {
        // Arrange
        var mockDatabaseAccess = new Mock<IDatabaseAccess<TagModel>>();

        // Act
        var sut = new TagsSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Assert
        sut.Should().NotBeNull();
        sut.Should().BeOfType<TagsSQLiteRepository>();
        sut.Should().BeAssignableTo<ITagsRepository>();
    }

    [Fact]
    public async Task TagsSQLiteRepository_GetAll_ShouldReturnEntities()
    {
        // Arrange
        var mockDatabaseAccess = new Mock<IDatabaseAccess<TagModel>>();
        mockDatabaseAccess.Setup(mock => mock.QueryAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<TagModel>());
        var sut = new TagsSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.GetAll();

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QueryAsync("""
            SELECT
                Id,
                Name,
                IsPermanent
            FROM Tags
        """), Times.Once);
    }

    [Fact]
    public async Task TagsSQLiteRepository_GetByIdValidEntity_ShouldReturnEntity()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<TagModel>>();
        mockDatabaseAccess.Setup(mock => mock.QuerySingleOrDefaultAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(new TagModel());
        var sut = new TagsSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.GetById(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QuerySingleOrDefaultAsync("""
            SELECT
                Id,
                Name,
                IsPermanent
            FROM Tags
            WHERE Id = @Id
        """, 
            It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") ==id)),
            Times.Once);
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task TagsSQLiteRepository_GetByIdInvalidEntity_ShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<TagModel>>();
        mockDatabaseAccess.Setup(mock => mock.QuerySingleOrDefaultAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync((TagModel?)null);
        var sut = new TagsSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.GetById(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QuerySingleOrDefaultAsync("""
            SELECT
                Id,
                Name,
                IsPermanent
            FROM Tags
            WHERE Id = @Id
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == id)),
            Times.Once);
        result.Should().BeNull();
    }

    [Fact]
    public async Task TagsSQLiteRepository_CreateValidEntity_ShouldReturnGuid()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new Tag { IsPermanent = true, Name = "test" };

        var mockDatabaseAccess = new Mock<IDatabaseAccess<TagModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(1);
        var sut = new TagsSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Create(entity);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            INSERT INTO Tags (Id, Name, IsPermanent)
            VALUES (@Id, @Name, @IsPermanent)
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<bool>("IsPermanent") == entity.IsPermanent
                && p.Get<string>("Name") == entity.Name)),
            Times.Once);
    }

    [Fact]
    public async Task TagsSQLiteRepository_UpdateValidEntity_ShouldReturnTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new Tag { Id = id, IsPermanent = true, Name = "test" };

        var mockDatabaseAccess = new Mock<IDatabaseAccess<TagModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(1);
        var sut = new TagsSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Update(entity);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            UPDATE Tags
            SET Name = @Name,
                IsPermanent = @IsPermanent
            WHERE Id = @Id
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == entity.Id
                &&  p.Get<bool>("IsPermanent") == entity.IsPermanent
                && p.Get<string>("Name") == entity.Name)),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task TagsSQLiteRepository_UpdateInvalidEntity_ShouldReturnFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new Tag { Id = id, IsPermanent = true, Name = "test" };

        var mockDatabaseAccess = new Mock<IDatabaseAccess<TagModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(0);
        var sut = new TagsSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Update(entity);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            UPDATE Tags
            SET Name = @Name,
                IsPermanent = @IsPermanent
            WHERE Id = @Id
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == entity.Id
                && p.Get<bool>("IsPermanent") == entity.IsPermanent
                && p.Get<string>("Name") == entity.Name)),
            Times.Once);
        result.Should().BeFalse();
    }

    [Fact]
    public async Task TagsSQLiteRepository_DeleteValidEntity_ShouldReturnTrue()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<TagModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(1);
        var sut = new TagsSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Delete(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            DELETE FROM Tags
            WHERE Id = @Id
        """,
        It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == id)),
        Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task TagsSQLiteRepository_DeleteInvalidEntity_ShouldReturnFalse()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<TagModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(0);
        var sut = new TagsSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Delete(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            DELETE FROM Tags
            WHERE Id = @Id
        """,
        It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == id)),
        Times.Once);
        result.Should().BeFalse();
    }
}
