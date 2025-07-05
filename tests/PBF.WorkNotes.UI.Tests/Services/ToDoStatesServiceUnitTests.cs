namespace PBF.WorkNotes.UI.Tests.Services;

[ExcludeFromCodeCoverage]
[Trait("Unit Tests", "UI")]
public class ToDoStatesServiceUnitTests : BaseServiceUnitTests
{
    public ToDoStatesServiceUnitTests() : base()
    {
    }

    [Fact]
    public void ToDoStatesService_Constructor_SchouldCreateInstance()
    {
        // Arrange
        var mockGetAllToDoStatesUseCase = new Mock<IGetAllToDoStatesUseCase>();

        // Act
        var sut = new ToDoStatesService(Mapper, mockGetAllToDoStatesUseCase.Object);

        // Assert
        sut.Should().NotBeNull();
        sut.Should().BeOfType<ToDoStatesService>();
        sut.Should().BeAssignableTo<IToDoStatesService>();
    }

    [Fact]
    public async Task ToDoStatesService_GetAsync_ShouldReturnEntities()
    {
        // Arrange
        var mockGetAllToDoStatesUseCase = new Mock<IGetAllToDoStatesUseCase>();
        var sut = new ToDoStatesService(Mapper, mockGetAllToDoStatesUseCase.Object);
        mockGetAllToDoStatesUseCase.Setup(mock => mock.Execute()).ReturnsAsync(new List<Entities.ToDoState>
        {
            new Entities.ToDoState { Id = Guid.NewGuid(), Name = "To Do" },
            new Entities.ToDoState { Id = Guid.NewGuid(), Name = "In Progress" },
            new Entities.ToDoState { Id = Guid.NewGuid(), Name = "Done" }
        });

        // Act
        var result = sut.GetAsync();

        // Assert
        var resultList = new List<ToDoState>();
        await foreach (var item in result)
        {
            resultList.Add(item);
        }
        resultList.Should().NotBeNull();
        resultList.Should().HaveCount(3);
        resultList.Should().Contain(t => t.Name == "To Do");
        resultList.Should().Contain(t => t.Name == "In Progress");
        resultList.Should().Contain(t => t.Name == "Done");
    }
}
