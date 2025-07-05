namespace PBF.WorkNotes.UI.Tests.Services;

[ExcludeFromCodeCoverage]
[Trait("Unit Tests", "UI")]
public class PrioritiesServiceUnitTests : BaseServiceUnitTests
{
    public PrioritiesServiceUnitTests() : base()
    {
    }

    [Fact]
    public void PrioritiesService_Constructor_SchouldCreateInstance()
    {
        // Arrange
        var mockGetAllPrioritiesUseCase = new Mock<IGetAllPrioritiesUseCase>();

        // Act
        var sut = new PrioritiesService(Mapper, mockGetAllPrioritiesUseCase.Object);

        // Assert
        sut.Should().NotBeNull();
        sut.Should().BeOfType<PrioritiesService>();
        sut.Should().BeAssignableTo<IPrioritiesService>();
    }

    [Fact]
    public async Task PrioritiesService_GetAsync_ShouldReturnEntities()
    {
        // Arrange
        var mockGetAllPrioritiesUseCase = new Mock<IGetAllPrioritiesUseCase>();
        var sut = new PrioritiesService(Mapper, mockGetAllPrioritiesUseCase.Object);
        mockGetAllPrioritiesUseCase.Setup(mock =>mock.Execute()).ReturnsAsync(new List<Entities.Priority>
        {
            new Entities.Priority { Id = Guid.NewGuid(), Color = "#e57373", Name = "High" },
            new Entities.Priority { Id = Guid.NewGuid(), Color = "#f06292", Name = "Medium" },
            new Entities.Priority { Id = Guid.NewGuid(), Color = "#ba68c8", Name = "Low" }
        });

        // Act
        var result = sut.GetAsync();

        // Assert
        var resultList = new List<Priority>();
        await foreach (var item in result)
        {
            resultList.Add(item);
        }
        resultList.Should().NotBeNull();
        resultList.Should().HaveCount(3);
        resultList.Should().Contain(t => t.Name == "High");
        resultList.Should().Contain(t => t.Name == "Medium");
        resultList.Should().Contain(t => t.Name == "Low");
    }
}
