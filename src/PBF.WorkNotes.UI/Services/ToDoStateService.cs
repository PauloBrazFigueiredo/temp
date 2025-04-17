namespace PBF.WorkNotes.UI.Services;

public  class ToDoStateService : IToDoStateService
{
    private readonly IToDoStateRepository _toDoStateRepository;

    public ToDoStateService(IToDoStateRepository toDoStateRepository)
    {
        _toDoStateRepository = toDoStateRepository;
    }
}
