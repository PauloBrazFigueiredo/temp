global using TimeProvider = PBF.WorkNotes.Application.Providers.TimeProvider;

namespace PBF.WorkNotes.Application.Tests.Providers;

[ExcludeFromCodeCoverage]
public class TimeProviderUnitTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance()
    {
        // Act
        var sut = new TimeProvider();

        // Arrange
        sut.Should().BeOfType<TimeProvider>();
        sut.Should().BeAssignableTo<ITimeProvider>();
    }

    [Fact]
    public void GetUtcNow_ShouldReturnDateTime()
    {
        // Arrange
        var sut = new TimeProvider();

        // Act
        var result = sut.GetUtcNow();

        // Arrange
        result.Should().BeCloseTo(DateTime.UtcNow, new TimeSpan(0, 1, 0));
    }
}
