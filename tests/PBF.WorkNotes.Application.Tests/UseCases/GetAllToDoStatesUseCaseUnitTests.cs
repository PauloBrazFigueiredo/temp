namespace PBF.WorkNotes.Application.Tests.UseCases;

[ExcludeFromCodeCoverage]
public class GetAllToDoStatesUseCaseUnitTests
{
    [Fact]
    public void Constructor_ShouldCreateInstance()
    {
        // Act
        var sut = new GetAllToDoStatesUseCase();

        // Arrange
        sut.Should().BeOfType<GetAllToDoStatesUseCase>();
        sut.Should().BeAssignableTo<IGetAllToDoStatesUseCase>();
    }
}
