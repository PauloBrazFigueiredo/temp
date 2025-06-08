namespace PBF.WorkNotes.Application.Tests.Factories;

[ExcludeFromCodeCoverage]
[Trait("Unit Tests", "Application")]
public class ToDoFactoryUnitTests
{
    [Fact]
    public void ToDoFactory_ShouldCreateInstance()
    {
        // Arrange
        var mockTimeProvider = new Mock<ITimeProvider>();
        var mockGuidProvider = new Mock<GuidProvider>();

        // Act
        var sut = new ToDoFactory(mockTimeProvider.Object, mockGuidProvider.Object);

        // Arrange
        sut.Should().BeOfType<ToDoFactory>();
        sut.Should().BeAssignableTo<IToDoFactory>();
    }

    [Fact]
    public void ToDoFactory_CreateInstance_ShouldReturnToDo()
    {
        // Arrange
        var mockTimeProvider = new Mock<ITimeProvider>();
        mockTimeProvider.Setup(mock => mock.GetUtcNow())
            .Returns(new DateTime(1990, 4, 20));
        var mockGuidProvider = new Mock<GuidProvider>();
        var sut = new ToDoFactory(mockTimeProvider.Object, mockGuidProvider.Object);

        var now = DateTime.UtcNow;

        // Act
        var result = sut.CreateInstance();

        // Arrange
        result.Should().BeOfType<ToDo>();
        result.Id.Should().NotBeEmpty();
        result.Title.Should().BeEmpty();
        result.Notes.Should().BeEmpty();
        result.State.Should().BeOfType<ToDoState>();
        //TODO: Implement StateRepository
        //result.State.Id.Should().Be("Active");
        result.State.Name.Should().Be("Active");
        result.Tags.Should().BeEmpty();
        result.Priority.Should().BeOfType<Priority>();
        //TODO: Implement TodoPriorityRepository
        //result.Priority.Id.Should().Be();
        result.Order.Should().Be(0);
        result.WorkDate.Should().Be(new DateTime(1990, 4, 20));
        result.DueDate.Should().BeNull();
        result.CreatedDate.Should().Be(new DateTime(1990, 4, 20));
    }
}