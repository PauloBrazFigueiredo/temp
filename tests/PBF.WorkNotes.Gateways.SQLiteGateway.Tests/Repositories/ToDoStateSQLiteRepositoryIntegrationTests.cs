namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Repositories;

[ExcludeFromCodeCoverage]
[Trait("Integration Tests", "Gateways")]
[Collection("ToDoStateSQLiteRepository")]
public class ToDoStateSQLiteRepositoryIntegrationTests
{
    private readonly IMapper _mapper;
    private readonly IGuidProvider _guidProvider;

    public ToDoStateSQLiteRepositoryIntegrationTests()
    {
        var settingsProvider = CreateSettingsProvider();
        var serviceProvider = CreateServiceProvider(settingsProvider);
        UpdateDatabase(serviceProvider);
        
        var toDoStateRepository = serviceProvider.GetRequiredService<IToDoStateRepository>();
    }

    private ISettingsProvider CreateSettingsProvider()
    {
        return new SettingsProvider
        {
            Settings = new AppSettings
            {
                ConnectionStrings = new List<ConnectionStringSettings>
                {
                    new ConnectionStringSettings
                    {
                        Name = "WorkNotesData",
                        ConnectionString = "Data Source=:memory:"
                    }
                }
            }
        };
    }

    private IServiceProvider CreateServiceProvider(ISettingsProvider settingsProvider)
    {
        var a = new Migration_2025032201();

        var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == "PBF.WorkNotes.Gateways.SQLiteMigrator");

        return new ServiceCollection()
            .AddFluentMigratorCore()
            .AddAutoMapperProfiles()
            .AddGuidProvider()
            .AddSingleton<AppSettings>(settingsProvider.Settings)
            .AddSQLiteGateway(settingsProvider)
            .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(settingsProvider.GetWorkNotesDataDatabaseConnectionString())
                .ScanIn(assembly).For.Migrations())
                .BuildServiceProvider();
    }

    private void UpdateDatabase(IServiceProvider serviceProvider)
    {
        var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    [Fact]
    public void ToDoStateSQLiteRepository_GetAll_SchouldReturnEntities()
    {
        //    // Arrange
        //    var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel, Guid>>();

        //    // Act
        //    var sut = new ToDoStateSQLiteRepository(_mapper, _guidProvider, mockDatabaseAccess.Object);

        //    // Assert
        //    sut.Should().NotBeNull();
        //    sut.Should().BeOfType<ToDoStateSQLiteRepository>();
        //    sut.Should().BeAssignableTo<IToDoStateRepository>();
        //}

        //[Fact]
        //public void ToDoStateSQLiteRepository_Constructor_WithNullMapper_SchouldThrowArgumentNullException()
        //{
        //    // Arrange
        //    var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel, Guid>>();

        //    // Act
        //    Action action = () => new ToDoStateSQLiteRepository(null, _guidProvider, mockDatabaseAccess.Object);

        //    // Assert
        //    action.Should().Throw<ArgumentNullException>()
        //        .WithMessage("Value cannot be null. (Parameter 'mapper')"); 
    }

    //[Fact]
    //public void ToDoStateSQLiteRepository_Constructor_WithNullGuidProvider_SchouldThrowArgumentNullException()
    //{
    //    // Arrange
    //    var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel, Guid>>();

    //    // Act
    //    Action action = () => new ToDoStateSQLiteRepository(_mapper, null, mockDatabaseAccess.Object);

    //    // Assert
    //    action.Should().Throw<ArgumentNullException>()
    //        .WithMessage("Value cannot be null. (Parameter 'guidProvider')");
    //}

    //[Fact]
    //public void ToDoStateSQLiteRepository_Constructor_WithNullDatabaseAccess_SchouldThrowArgumentNullException()
    //{
    //    // Arrange
    //    var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel, Guid>>();

    //    // Act
    //    Action action = () => new ToDoStateSQLiteRepository(_mapper, _guidProvider, null);

    //    // Assert
    //    action.Should().Throw<ArgumentNullException>()
    //        .WithMessage("Value cannot be null. (Parameter 'databaseAccess')");
    //}

    //[Fact]
    //public async Task ToDoStateSQLiteRepository_GetAll()
    //{
    //    // Arrange
    //    var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel, Guid>>();
    //    mockDatabaseAccess.Setup(mock => mock.QueryAsync(It.IsAny<string>()))
    //        .ReturnsAsync(new List<ToDoStateModel>());
    //    var sut = new ToDoStateSQLiteRepository(_mapper, _guidProvider, mockDatabaseAccess.Object);

    //    // Act
    //    var result = await sut.GetAll();

    //    // Assert
    //    mockDatabaseAccess.Verify(mock => mock.QueryAsync("""
    //        SELECT
    //            Id,
    //            Name,
    //            IsDefault
    //        FROM ToDoState
    //    """), Times.Once);
    //}

    //[Fact]
    //public async Task ToDoStateSQLiteRepository_GetById()
    //{
    //    // Arrange
    //    var id = Guid.NewGuid();

    //    var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel, Guid>>();
    //    mockDatabaseAccess.Setup(mock => mock.QuerySingleOrDefaultAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
    //        .ReturnsAsync(new ToDoStateModel());
    //    var sut = new ToDoStateSQLiteRepository(_mapper, _guidProvider, mockDatabaseAccess.Object);

    //    // Act
    //    var result = await sut.GetById(id);

    //    // Assert
    //    mockDatabaseAccess.Verify(mock => mock.QuerySingleOrDefaultAsync("""
    //        SELECT
    //            Id,
    //            Name,
    //            IsDefault
    //        FROM ToDoState
    //        WHERE Id = @id
    //    """, 
    //        It.Is<DynamicParameters>(p =>
    //            p.Get<Guid>("Id") ==id)),
    //        Times.Once);
    //}

    //[Fact]
    //public async Task ToDoStateSQLiteRepository_Create()
    //{
    //    // Arrange
    //    var id = Guid.NewGuid();
    //    var entity = new ToDoState { IsDefault = true, Name = "test" };

    //    var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel, Guid>>();
    //    mockDatabaseAccess.Setup(mock => mock.QuerySingleOrDefaultAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
    //        .ReturnsAsync(new ToDoStateModel());
    //    var sut = new ToDoStateSQLiteRepository(_mapper, _guidProvider, mockDatabaseAccess.Object);

    //    // Act
    //    var result = await sut.Create(entity);

    //    // Assert
    //    mockDatabaseAccess.Verify(mock => mock.InsertAndGetIdAsync("""
    //        INSERT INTO ToDoState (Id, Name, IsDefault)
    //        VALUES (@Id, @Name, @IsDefault)
    //    """,
    //        It.Is<DynamicParameters>(p =>
    //            p.Get<bool>("IsDefault") == entity.IsDefault
    //            && p.Get<string>("Name") == entity.Name)),
    //        Times.Once);
    //}

    //[Fact]
    //public async Task ToDoStateSQLiteRepository_Update()
    //{
    //    // Arrange
    //    var id = Guid.NewGuid();
    //    var entity = new ToDoState { Id = id, IsDefault = true, Name = "test" };

    //    var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel, Guid>>();
    //    mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
    //        .ReturnsAsync(1);
    //    var sut = new ToDoStateSQLiteRepository(_mapper, _guidProvider, mockDatabaseAccess.Object);

    //    // Act
    //    var result = await sut.Update(entity);

    //    // Assert
    //    mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
    //        UPDATE ToDoState 
    //        SET Name = @Name,
    //            IsDefault = @IsDefault
    //        WHERE Id = @Id
    //    """,
    //        It.Is<DynamicParameters>(p =>
    //            p.Get<Guid>("Id") == entity.Id
    //            &&  p.Get<bool>("IsDefault") == entity.IsDefault
    //            && p.Get<string>("Name") == entity.Name)),
    //        Times.Once);
    //    result.Should().Be(1);
    //}

    //[Fact]
    //public async Task ToDoStateSQLiteRepository_Delete()
    //{
    //    // Arrange
    //    var id = Guid.NewGuid();

    //    var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel, Guid>>();
    //    mockDatabaseAccess.Setup(mock => mock.ExecuteAsync(It.IsAny<string>(), It.IsAny<DynamicParameters>()))
    //        .ReturnsAsync(1);
    //    var sut = new ToDoStateSQLiteRepository(_mapper, _guidProvider, mockDatabaseAccess.Object);

    //    // Act
    //    var result = await sut.Delete(id);

    //    // Assert
    //    mockDatabaseAccess.Verify(mock => mock.ExecuteAsync("""
    //        DELETE FROM ToDoState 
    //        WHERE Id = @Id
    //    """,
    //    It.Is<DynamicParameters>(p =>
    //            p.Get<Guid>("Id") == id)),
    //    Times.Once);
    //    result.Should().Be(1);
    //}

    //private IMapper CreateMapper()
    //{
    //    var config = new MapperConfiguration(cfg =>
    //    {
    //        cfg.AddProfile<MappingProfile>();
    //    });
    //    return config.CreateMapper();
    //}

    //private IGuidProvider CreateGuidProvider()
    //{
    //    var mock = new Mock<IGuidProvider>();
    //    mock.Setup(mock => mock.GetGuid()).Returns(Guid.NewGuid());
    //    return mock.Object;
    //}   
}
