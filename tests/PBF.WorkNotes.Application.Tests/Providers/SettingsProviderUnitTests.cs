using PBF.WorkNotes.Application.Settings;

namespace PBF.WorkNotes.Application.Tests.Providers;

[ExcludeFromCodeCoverage]
public class SettingsProviderUnitTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance()
    {
        // Act
        var sut = new SettingsProvider();

        // Arrange
        sut.Should().BeOfType<SettingsProvider>();
        sut.Should().BeAssignableTo<ISettingsProvider>();
    }

    [Fact]
    public void GetConnectionString_WithExistingConnectionString_ShouldReturnString()
    {
        // Arrange
        var appSettings = new AppSettings
        {
            ConnectionStrings = new List<ConnectionStringSettings>
            {
                new ConnectionStringSettings { Name = "ConnectionStringName", ConnectionString = "ConnectionString" }
            }
        };

        // Act
        var result = SettingsProvider.GetConnectionString(appSettings, "ConnectionStringName");

        // Assert
        result.Should().Be("ConnectionString");
    }

    [Fact]
    public void GetConnectionString_WithoutConnectionString_ShouldReturnEmptyString()
    {
        // Arrange
        var appSettings = new AppSettings();

        // Act
        var result = SettingsProvider.GetConnectionString(appSettings, "ConnectionStringName");

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GetWorkNotesDataDatabaseConnectionString_WithExistingConnectionString_ShouldReturnString()
    {
        // Arrange
        var sut = new SettingsProvider();
        sut.Settings = new AppSettings
        {
            ConnectionStrings = new List<ConnectionStringSettings>
            {
                new ConnectionStringSettings { Name = "WorkNotesData", ConnectionString = "ConnectionString" }
            }
        };

        // Act
        var result = sut.GetWorkNotesDataDatabaseConnectionString();

        // Assert
        result.Should().Be("ConnectionString");
    }
}
