namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Repositories;

[ExcludeFromCodeCoverage]
[Trait("Unit Tests", "Gateways")]
public class ToDoStatesSQLiteRepositoryUnitTests : BaseSQLiteRepositoryUnitTests
{
    private readonly IMapper _mapper;
    private readonly IGuidProvider _guidProvider;

    public ToDoStatesSQLiteRepositoryUnitTests()
    {
        _mapper = CreateMapper();
        _guidProvider = CreateGuidProvider();
    }

    [Fact]
    public void ToDoStatesSQLiteRepository_Constructor_SchouldCreateInstance()
    {
        // Arrange
        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel>>();

        // Act
        var sut = new ToDoStatesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Assert
        sut.Should().NotBeNull();
        sut.Should().BeOfType<ToDoStatesSQLiteRepository>();
        sut.Should().BeAssignableTo<IToDoStatesRepository>();
    }

    [Fact]
    public async Task ToDoStatesSQLiteRepository_GetAll_ShouldreturnEntities()
    {
        // Arrange
        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel>>();
        mockDatabaseAccess.Setup(mock => mock.QueryAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<ToDoStateModel>());
        var sut = new ToDoStatesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.GetAll();

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QueryAsync("""
            SELECT
                Id,
                Name,
                IsDefault
            FROM ToDoStates
        """), Times.Once);
    }

    [Fact]
    public async Task ToDoStatesSQLiteRepository_GetByIdValidEntity_ShouldReturnEntity()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel>>();
        mockDatabaseAccess.Setup(mock => mock.QuerySingleOrDefaultAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(new ToDoStateModel());
        var sut = new ToDoStatesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.GetById(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QuerySingleOrDefaultAsync("""
            SELECT
                Id,
                Name,
                IsDefault
            FROM ToDoStates
            WHERE Id = @Id
        """, 
            It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") ==id)),
            Times.Once);
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task ToDoStatesSQLiteRepository_GetByIdInvalidEntity_ShouldReturnNull()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel>>();
        mockDatabaseAccess.Setup(mock => mock.QuerySingleOrDefaultAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync((ToDoStateModel)null);
        var sut = new ToDoStatesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.GetById(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QuerySingleOrDefaultAsync("""
            SELECT
                Id,
                Name,
                IsDefault
            FROM ToDoStates
            WHERE Id = @Id
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == id)),
            Times.Once);
        result.Should().BeNull();
    }

    [Fact]
    public async Task ToDoStatesSQLiteRepository_CreateValidEntity_ShouldReturnGuid()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new ToDoState { IsDefault = true, Name = "test" };

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(1);
        var sut = new ToDoStatesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Create(entity);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            INSERT INTO ToDoStates (Id, Name, IsDefault)
            VALUES (@Id, @Name, @IsDefault)
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<bool>("IsDefault") == entity.IsDefault
                && p.Get<string>("Name") == entity.Name)),
            Times.Once);
    }

    [Fact]
    public async Task ToDoStatesSQLiteRepository_UpdateValidEntity_ShouldReturnTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new ToDoState { Id = id, IsDefault = true, Name = "test" };

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(1);
        var sut = new ToDoStatesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Update(entity);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            UPDATE ToDoStates
            SET Name = @Name,
                IsDefault = @IsDefault
            WHERE Id = @Id
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == entity.Id
                &&  p.Get<bool>("IsDefault") == entity.IsDefault
                && p.Get<string>("Name") == entity.Name)),
            Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task ToDoStatesSQLiteRepository_UpdateInvalidEntity_ShouldReturnFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new ToDoState { Id = id, IsDefault = true, Name = "test" };

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(0);
        var sut = new ToDoStatesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Update(entity);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            UPDATE ToDoStates
            SET Name = @Name,
                IsDefault = @IsDefault
            WHERE Id = @Id
        """,
            It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == entity.Id
                && p.Get<bool>("IsDefault") == entity.IsDefault
                && p.Get<string>("Name") == entity.Name)),
            Times.Once);
        result.Should().BeFalse();
    }

    [Fact]
    public async Task ToDoStatesSQLiteRepository_DeleteValidEntity_ShouldReturnTrue()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(1);
        var sut = new ToDoStatesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Delete(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            DELETE FROM ToDoStates
            WHERE Id = @Id
        """,
        It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == id)),
        Times.Once);
        result.Should().BeTrue();
    }

    [Fact]
    public async Task ToDoStatesSQLiteRepository_DeleteInvalidEntity_ShouldReturnFalse()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel>>();
        mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
            .ReturnsAsync(0);
        var sut = new ToDoStatesSQLiteRepository(mockDatabaseAccess.Object, _mapper, _guidProvider);

        // Act
        var result = await sut.Delete(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
            DELETE FROM ToDoStates
            WHERE Id = @Id
        """,
        It.Is<DynamicParameters>(p =>
                p.Get<Guid>("Id") == id)),
        Times.Once);
        result.Should().BeFalse();
    }
}
