using Microsoft.Data.Sqlite;
//using static System.Collections.Specialized.BitVector32;

namespace PBF.WorkNotes.Gateways.SQLiteGateway.Tests.Helpers;

[ExcludeFromCodeCoverage]
[Trait("Unit Tests", "Gateways")]
public class SQLiteDatabaseAccessUnitTests
{
    [Fact]
    public void SQLiteDatabaseAccess_Constructor_SchouldCreateInstance()
    {
        // Arrange
        var settingsProvider = CreateSettingsProvider();

        // Act
        var sut = new SQLiteDatabaseAccess<StubDummy>(settingsProvider);
        
        // Assert
        sut.Should().NotBeNull();
        sut.Should().BeOfType<SQLiteDatabaseAccess<StubDummy>>();
        sut.Should().BeAssignableTo<IDatabaseAccess<StubDummy>>();
    }

    [Fact]
    public void SQLiteDatabaseAccess_OpenConnection_SchouldReturnConnection()
    {
        // Arrange
        var settingsProvider = CreateSettingsProvider();
        var sut = new SQLiteDatabaseAccess<StubDummy>(settingsProvider);

        // Act
        var result = sut.OpenConnection();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<SqliteConnection>();
        result.Should().BeAssignableTo<IDbConnection>();
    }

    [Fact]
    public void SQLiteDatabaseAccess_CloseConnection_SchouldCloseConnection()
    {
        // Arrange
        var settingsProvider = CreateSettingsProvider();
        var sut = new SQLiteDatabaseAccess<StubDummy>(settingsProvider);
        var connection = sut.OpenConnection();

        // Act
        sut.CloseConnection();

        // Assert
        connection.State.Should().Be(ConnectionState.Closed);
    }

    [Fact]
    public void SQLiteDatabaseAccess_CloseDisposedConnection_SchouldDoNothing()
    {
        // Arrange
        var settingsProvider = CreateSettingsProvider();
        var sut = new SQLiteDatabaseAccess<StubDummy>(settingsProvider);
        var connection = sut.OpenConnection();
        connection.Dispose();

        // Act
        sut.CloseConnection();

        // Assert
        connection.State.Should().Be(ConnectionState.Closed);
    }

    [Fact]
    public void SQLiteDatabaseAccess_CloseNullConnection_SchouldDoNothing()
    {
        // Arrange
        var settingsProvider = CreateSettingsProvider();
        var sut = new StubSQLiteDatabaseAccess<StubDummy>(settingsProvider);
        sut.Connection = null;

        // Act
        sut.CloseConnection();

        // Assert
        sut.Connection.Should().BeNull();
    }

    [Fact]
    public async Task SQLiteDatabaseAccess_QueryAsync_SchouldReturnEntities()
    {
        // Arrange
        var settingsProvider = CreateSettingsProvider();
        var sut = new StubSQLiteDatabaseAccess<StubDummy>(settingsProvider);
//        var mockConnection = new Mock<IDbConnection>();
        //mockConnection.Setup(c => c.QueryAsync<StubDummy>(
        //    It.IsAny<string>(),
        //    It.IsAny<object>(),
        //    It.IsAny<IDbTransaction>(),
        //    It.IsAny<int?>(),
        //    It.IsAny<CommandType?>()));
        //mockConnection.Setup(c => c.QueryAsync<StubDummy>("query"))
        //    .ReturnsAsync(new List<StubDummy> { new StubDummy() });


        // ...

        //mockConnection.Setup(c => c.QueryAsync<StubDummy>("query", null, null, null, null))
        //    .ReturnsAsync(new List<StubDummy> { new StubDummy() });
        //mockConnection.Setup(c => c.QueryAsync<StubDummy>("query"))
        //    .ReturnsAsync(new List<StubDummy> { new StubDummy() });
   //     sut.Connection = mockConnection.Object;

        // Act
        var result = await sut.QueryAsync("SELECT 'Hello world'");

        // Assert
        //mockConnection.Verify(c => c.QueryAsync<StubDummy>("query", null, null, null, null), Times.Once);
    }

    private ISettingsProvider CreateSettingsProvider()
    {
        var settings = new AppSettings();
        settings.ConnectionStrings.Add(new ConnectionStringSettings
        {
            Name = "WorkNotesData",
            ConnectionString = "Data Source=:memory:;"
        });

        var mockSettingsProvider = new Mock<ISettingsProvider>();
        mockSettingsProvider.Setup(sp => sp.Settings).Returns(settings);
        return mockSettingsProvider.Object;
    }
}
