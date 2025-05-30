namespace PBF.WorkNotes.Application.Factories;

public class ToDoFactory : IToDoFactory
{
    private readonly ITimeProvider _timeProvider;
    private readonly IGuidProvider _guidProvider;

    public ToDoFactory(ITimeProvider timeProvider, IGuidProvider guidProvider)
    {
        _timeProvider = timeProvider;
        _guidProvider = guidProvider;
    }

    public ToDo CreateInstance()
    {
        return new ToDo
        {
            Id = _guidProvider.GetGuid(),
            Title = string.Empty,
            Description = string.Empty,
            State = new ToDoState { Name = "Active" },
            Tags = new List<Tag>(),
            Priority = new Priority { Name = "None" },
            Order = 0,
            WorkDate = _timeProvider.GetUtcNow(),
            DueDate = null,
            CreatedDate = _timeProvider.GetUtcNow()
        };
    }
}
