namespace PBF.WorkNotes.Application.Tests.Providers;

[ExcludeFromCodeCoverage]
[Trait("Unit Tests", "Application")]
public class GuidProviderUnitTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance()
    {
        // Act
        var sut = new GuidProvider();

        // Arrange
        sut.Should().BeOfType<GuidProvider>();
        sut.Should().BeAssignableTo<IGuidProvider>();
    }

    [Fact]
    public void GetGuid_ShouldReturnGuid()
    {
        // Arrange
        var sut = new GuidProvider();

        // Act
        var result = sut.GetGuid();

        // Arrange
        result.Should().NotBeEmpty();
    }
}
