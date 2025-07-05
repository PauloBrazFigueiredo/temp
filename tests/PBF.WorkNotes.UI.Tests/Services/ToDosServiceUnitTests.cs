using System.Windows.Media;

namespace PBF.WorkNotes.UI.Tests.Services;

[ExcludeFromCodeCoverage]
[Trait("Unit Tests", "UI")]
public class ToDosServiceUnitTests : BaseServiceUnitTests
{
    public ToDosServiceUnitTests() : base()
    {
    }

    [Fact]
    public void ToDosService_Constructor_SchouldCreateInstance()
    {
        // Arrange
        var mockGetToDoUseCase = new Mock<IGetToDoUseCase>();
        var mockCreateToDoUseCase = new Mock<ICreateToDoUseCase>();
        var mockUpdateToDoUseCase = new Mock<IUpdateToDoUseCase>();
        var mockGetPriorityUseCase = new Mock<IGetPriorityUseCase>();
        var mockGetToDoStateUseCase = new Mock<IGetToDoStateUseCase>();

        // Act
        var sut = new ToDosService(Mapper,
            mockGetToDoUseCase.Object,
            mockCreateToDoUseCase.Object,
            mockUpdateToDoUseCase.Object,
            mockGetPriorityUseCase.Object,
            mockGetToDoStateUseCase.Object);

        // Assert
        sut.Should().NotBeNull();
        sut.Should().BeOfType<ToDosService>();
        sut.Should().BeAssignableTo<IToDosService>();
    }

    [Fact]
    public async Task  GetAsync_GetValidToDo_SchouldReturnModel()
    {
        // Arrange
        var priorityId = Guid.NewGuid();
        var stateId = Guid.NewGuid();
        var mockGetToDoUseCase = new Mock<IGetToDoUseCase>();
        mockGetToDoUseCase.Setup(mock => mock.Execute(It.IsAny<Guid>()))
            .ReturnsAsync(new Entities.ToDo
            {
                Id = Guid.NewGuid(),
                Title = "Test ToDo",
                Notes = "Test Notes",
                PriorityId = priorityId,
                Priority = new Entities.Priority
                {
                    Id = priorityId,
                    Name = "High"
                },
                StateId = stateId,
                State = new Entities.ToDoState
                {
                    Id = stateId,
                    Name = "Open"
                },
            });
        var mockCreateToDoUseCase = new Mock<ICreateToDoUseCase>();
        var mockUpdateToDoUseCase = new Mock<IUpdateToDoUseCase>();
        var mockGetPriorityUseCase = new Mock<IGetPriorityUseCase>();
        mockGetPriorityUseCase.Setup(mock => mock.Execute(It.IsAny<Guid>()))
            .ReturnsAsync(new Entities.Priority
            {
                Id = priorityId,
                Name = "High"
            });
        var mockGetToDoStateUseCase = new Mock<IGetToDoStateUseCase>();
        mockGetToDoStateUseCase.Setup(Priority => Priority.Execute(It.IsAny<Guid>()))
            .ReturnsAsync(new Entities.ToDoState
            {
                Id = stateId,
                Name = "Open"
            });

        var sut = new ToDosService(Mapper,
            mockGetToDoUseCase.Object,
            mockCreateToDoUseCase.Object,
            mockUpdateToDoUseCase.Object,
            mockGetPriorityUseCase.Object,
            mockGetToDoStateUseCase.Object);

        // Act
        var result = await sut.GetAsync(Guid.NewGuid());

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ToDo>();
    }

    [Fact]
    public async Task CreateAsync_WithValidToDo_SchouldReturnModel()
    {
        // Arrange
        var model =new ToDo
        {
            Id = Guid.NewGuid(),
            Title = "Test ToDo",
            Notes = "Test Notes",
            Priority = new Priority(),
            State = new ToDoState()
        };

        var priorityId = Guid.NewGuid();
        var stateId = Guid.NewGuid();
        var mockGetToDoUseCase = new Mock<IGetToDoUseCase>();
        var mockCreateToDoUseCase = new Mock<ICreateToDoUseCase>();
        mockCreateToDoUseCase.Setup(mock => mock.Execute(It.IsAny<Entities.ToDo>()))
            .ReturnsAsync(model.Id);
        var mockUpdateToDoUseCase = new Mock<IUpdateToDoUseCase>();
        var mockGetPriorityUseCase = new Mock<IGetPriorityUseCase>();
        var mockGetToDoStateUseCase = new Mock<IGetToDoStateUseCase>();

        var sut = new ToDosService(Mapper,
            mockGetToDoUseCase.Object,
            mockCreateToDoUseCase.Object,
            mockUpdateToDoUseCase.Object,
            mockGetPriorityUseCase.Object,
            mockGetToDoStateUseCase.Object);

        // Act
        var result = await sut.CreateAsync(model);

        // Assert
        result.Should().NotBeNull();
        result.Value.GetType().Should().Be(typeof(Guid));
    }

    [Fact]
    public async Task UpdateAsync_WithValidToDo_SchouldReturnTrue()
    {
        // Arrange
        var model = new ToDo
        {
            Id = Guid.NewGuid(),
            Title = "Test ToDo",
            Notes = "Test Notes",
            Priority = new Priority { Color = Brushes.Black },
            State = new ToDoState { }
        };

        var priorityId = Guid.NewGuid();
        var stateId = Guid.NewGuid();
        var mockGetToDoUseCase = new Mock<IGetToDoUseCase>();
        var mockCreateToDoUseCase = new Mock<ICreateToDoUseCase>();
        var mockUpdateToDoUseCase = new Mock<IUpdateToDoUseCase>();
        mockUpdateToDoUseCase.Setup(mock => mock.Execute(It.IsAny<Entities.ToDo>()))
            .ReturnsAsync(true);
        var mockGetPriorityUseCase = new Mock<IGetPriorityUseCase>();
        var mockGetToDoStateUseCase = new Mock<IGetToDoStateUseCase>();

        var sut = new ToDosService(Mapper,
            mockGetToDoUseCase.Object,
            mockCreateToDoUseCase.Object,
            mockUpdateToDoUseCase.Object,
            mockGetPriorityUseCase.Object,
            mockGetToDoStateUseCase.Object);

        // Act
        var result = await sut.UpdateAsync(model);

        // Assert
       result.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateAsync_WithInvalidToDo_SchouldReturnTrue()
    {
        // Arrange
        var model = new ToDo
        {
            Id = Guid.NewGuid(),
            Title = "Test ToDo",
            Notes = "Test Notes",
            Priority = new Priority { Color = Brushes.Black },
            State = new ToDoState { }
        };

        var priorityId = Guid.NewGuid();
        var stateId = Guid.NewGuid();
        var mockGetToDoUseCase = new Mock<IGetToDoUseCase>();
        var mockCreateToDoUseCase = new Mock<ICreateToDoUseCase>();
        var mockUpdateToDoUseCase = new Mock<IUpdateToDoUseCase>();
        mockUpdateToDoUseCase.Setup(mock => mock.Execute(It.IsAny<Entities.ToDo>()))
            .ReturnsAsync(false);
        var mockGetPriorityUseCase = new Mock<IGetPriorityUseCase>();
        var mockGetToDoStateUseCase = new Mock<IGetToDoStateUseCase>();

        var sut = new ToDosService(Mapper,
            mockGetToDoUseCase.Object,
            mockCreateToDoUseCase.Object,
            mockUpdateToDoUseCase.Object,
            mockGetPriorityUseCase.Object,
            mockGetToDoStateUseCase.Object);

        // Act
        var result = await sut.UpdateAsync(model);

        // Assert
        result.Should().BeFalse();
    }
}
