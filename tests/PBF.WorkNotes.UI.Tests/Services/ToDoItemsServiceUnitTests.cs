namespace PBF.WorkNotes.UI.Tests.Services;

[ExcludeFromCodeCoverage]
[Trait("Unit Tests", "UI")]
public class ToDoItemsServiceUnitTests : BaseServiceUnitTests
{  
    public ToDoItemsServiceUnitTests() : base()
    {
    }

    [Fact]
    public void ToDoItemsService_Constructor_SchouldCreateInstance()
    {
        // Arrange
        var mockGetToDoUseCase = new Mock<IGetToDoUseCase>();
        var mockGetPriorityUseCase = new Mock<IGetPriorityUseCase>();
        var mockGetToDoStateUseCase = new Mock<IGetToDoStateUseCase>();

        // Act
        var sut = new ToDoItemsService(
            Mapper,
            mockGetToDoUseCase.Object,
            mockGetPriorityUseCase.Object,
            mockGetToDoStateUseCase.Object);

        // Assert
        sut.Should().NotBeNull();
        sut.Should().BeOfType<ToDoItemsService>();
        sut.Should().BeAssignableTo<IToDoItemsService>();
    }

    [Fact]
    public async Task ToDoItemsService_GetAsync_ShouldReturnEntities()
    {
        // Arrange
        var mockGetToDoUseCase = new Mock<IGetToDoUseCase>();
        mockGetToDoUseCase.Setup(mock => mock.Execute())
            .ReturnsAsync(new List<Entities.ToDo>
            {
                new Entities.ToDo { Id = Guid.NewGuid(), Title = "Test Task 1", Priority = new Entities.Priority { Color = "#e57373" }, State = new Entities.ToDoState { Name = "State 1" } },
                new Entities.ToDo { Id = Guid.NewGuid(), Title = "Test Task 2", Priority = new Entities.Priority { Color = "#e57373" }, State = new Entities.ToDoState { Name = "State 2" } }
            });
        var mockGetPriorityUseCase = new Mock<IGetPriorityUseCase>();
        var mockGetToDoStateUseCase = new Mock<IGetToDoStateUseCase>();
        var sut = new ToDoItemsService(
            Mapper,
            mockGetToDoUseCase.Object,
            mockGetPriorityUseCase.Object,
            mockGetToDoStateUseCase.Object);

        // Act
        var result = sut.GetAsync();

        // Assert
        var resultList = new List<ToDoItem>();
        await foreach (var item in result)
        {
            resultList.Add(item);
        }
        resultList.Should().NotBeNull();
        resultList.Should().HaveCount(2);
        resultList.Should().Contain(t => t.Title == "Test Task 1");
        resultList.Should().Contain(t => t.Title == "Test Task 2");
    }
}
