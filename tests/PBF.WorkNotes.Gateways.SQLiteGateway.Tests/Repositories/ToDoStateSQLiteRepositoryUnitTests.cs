namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Repositories;

[ExcludeFromCodeCoverage]
public class ToDoStateSQLiteRepositoryUnitTests
{
    [Fact]
    public async Task ToDoStateSQLiteRepository_GetAll()
    {
        // Arrange
        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel>>();
        mockDatabaseAccess.Setup(mock => mock.QueryAsync(It.IsAny<string>()))
            .ReturnsAsync(new List<ToDoStateModel>());

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        var mapper = config.CreateMapper();

        var sut = new ToDoStateSQLiteRepository(mapper, mockDatabaseAccess.Object);

        // Act
        var result = await sut.GetAll();

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QueryAsync("""
            SELECT
                Id,
                Name,
                IsDefault
            FROM ToDoState
        """), Times.Once);
    }

    [Fact]
    public async Task ToDoStateSQLiteRepository_GetById()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mockDatabaseAccess = new Mock<IDatabaseAccess<ToDoStateModel>>();
        mockDatabaseAccess.Setup(mock => mock.QuerySingleOrDefaultAsync(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(new ToDoStateModel());

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        var mapper = config.CreateMapper();

        var sut = new ToDoStateSQLiteRepository(mapper, mockDatabaseAccess.Object);

        // Act
        var result = await sut.GetById(id);

        // Assert
        mockDatabaseAccess.Verify(mock => mock.QuerySingleOrDefaultAsync("""
            SELECT
                Id,
                Name,
                IsDefault
            FROM ToDoState
            WHERE Id = @id
        """, new { id }), Times.Once);
    }
}









// Arrange
//
//dataContext.CreateConnection();

//var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(assembly => assembly.GetName().Name == "PBF.WorkNotes.Gateways.SQLiteMigrator");

//var serviceProvider = new ServiceCollection()
//    .AddFluentMigratorCore()
//    .AddAutoMapperProfiles()
//    .ConfigureRunner(rb => rb
//        .AddSQLite()
//        .WithGlobalConnectionString(_databaseFixture.SettingsProvider.GetWorkNotesDataDatabaseConnectionString())
//        .ScanIn(assembly).For.Migrations())
//    .AddLogging(lb => lb.AddFluentMigratorConsole())
//    .BuildServiceProvider();

//var a = new Migration_2025032201();

//using (var scope = serviceProvider.CreateScope())
//{
//    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
//    runner.MigrateUp();

//    var mapper = serviceProvider.GetRequiredService<IMapper>();
//    var repository = new ToDoStateSQLiteRepository(mapper, dataContext);

//    var result = await repository.GetAll();
//}

//ar runner = serviceProvider.GetRequiredService<IMigrationRunner>();
//runner.MigrateUp();
//var mapper = serviceProvider.GetRequiredService<IMapper>();
//var repository = new ToDoStateSQLiteRepository(mapper, dataContext);

// Act
//try
//{
//    var result = await repository.GetAll();
//}
//catch (Exception ex)
//{
//    // Assert
//    Assert.True(false, ex.Message);
//}
// Assert
